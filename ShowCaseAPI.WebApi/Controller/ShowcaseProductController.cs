using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShowCaseAPI.Domain.Entities;
using ShowCaseAPI.Repositories.Interface;
using ShowCaseAPI.WebApi.Helper;
using ShowCaseAPI.WebApi.Model.Product;
using ShowCaseAPI.WebApi.Model.ShowcaseProduct;
using ShowCaseAPI.WebApi.Model.ShowcaseStyle;
using ShowCaseAPI.WebApi.Model.Store;
using ShowCaseAPI.WebApi.Model.User;
using System.Net;

namespace ShowCaseAPI.WebApi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ShowcaseProductController : ControllerBase
    {
        private readonly IShowcaseProductRepository _showcaseProductRepository;
        private readonly IShowcaseRepository _showcaseRepository;
        public ShowcaseProductController(IShowcaseProductRepository showcaseProductRepository, IShowcaseRepository showcaseRepository)
        {
            _showcaseProductRepository = showcaseProductRepository;
            _showcaseRepository = showcaseRepository;
        }


        [HttpGet("GetProductsByShowcaseId/{showCaseId}")]
        public async Task<IActionResult> GetByIdAsync(Guid showCaseId)
        {
            try
            {
                var result = _showcaseProductRepository.Query().Where(x => !x.Deleted && x.ShowcaseId == showCaseId).Select(x => new ShowcaseProductViewModel
                {
              
                    ShowcaseId = x.ShowcaseId,
                    StoreProductId = x.StoreProductId
                }).ToList();

                return ResponseHelper.Success(result);
            }
            catch (Exception e)
            {
                return ResponseHelper.InternalServerError(e.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> PutAsync([FromBody] PutShowcaseProductViewModel vm)
        {
            try
            {
                var showcase = await _showcaseRepository.GetById(vm.ShowcaseId);
                if (showcase == null)
                {
                    return ResponseHelper.BadRequest("Nenhuma vitrine encontrada!");
                };

                var oldShowcaseProducts = _showcaseProductRepository.Query().Where(x => !x.Deleted && x.ShowcaseId == vm.ShowcaseId).ToList();
                oldShowcaseProducts.ForEach(x => x.Deleted = true);

                foreach (var product in vm.ProductIds)
                {
                    var exist = oldShowcaseProducts.FirstOrDefault(x => x.StoreProductId == product);
                    if (exist != null)
                    {
                        oldShowcaseProducts.Remove(exist);
                        continue;
                    }

                    var created = await _showcaseProductRepository.Insert(new ShowcaseProduct
                    {
                        ShowcaseId = vm.ShowcaseId,
                        StoreProductId = product
                    });

                    if (created <= 0)
                    {
                        return ResponseHelper.BadRequest("Ocorreu um erro durante a exclusão dos produtos antigos.");
                    }
                }

                foreach (var oldProduct in oldShowcaseProducts)
                {
                    var removed = await _showcaseProductRepository.Delete(oldProduct.Id);
                    if (removed <= 0)
                    {
                        return ResponseHelper.BadRequest("Ocorreu um erro durante a exclusão dos produtos antigos.");
                    }
                }

                return ResponseHelper.Success();
            }
            catch (Exception e)
            {
                return ResponseHelper.InternalServerError(e.Message);
            }
        }
    }
}
