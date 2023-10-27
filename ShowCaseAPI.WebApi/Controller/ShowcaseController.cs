using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShowCaseAPI.Domain.Entities;
using ShowCaseAPI.Repositories.Interface;
using ShowCaseAPI.WebApi.Helper;
using ShowCaseAPI.WebApi.Model.Product;
using ShowCaseAPI.WebApi.Model.Showcase;
using ShowCaseAPI.WebApi.Model.Store;
using ShowCaseAPI.WebApi.Model.User;
using System.Net;

namespace ShowCaseAPI.WebApi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ShowcaseController : ControllerBase
    {
        private readonly IShowcaseRepository _showcaseRepository;
        private readonly IStoreRepository _storeRepository;
        private readonly IShowcaseStyleRepository _showcaseStyleRepository;
        public ShowcaseController(IShowcaseRepository showcaseRepository, IStoreRepository storeRepository, IShowcaseStyleRepository showcaseStyleRepository)
        {
            _showcaseRepository = showcaseRepository;
            _storeRepository = storeRepository;
            _showcaseStyleRepository = showcaseStyleRepository;
        }


        [HttpGet("GetShowcaseById/{id}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            try
            {
                var result = await _showcaseRepository.GetById(id);
                if (result == null)
                {
                    return ResponseHelper.BadRequest("Nenhum produto encontrado!");
                }

                return ResponseHelper.Success(new ShowcaseViewModel
                {
                    Id = result.Id,
                    ExclusiveCode = result.ExclusiveCode,
                    Name = result.Name,
                    StoreId = result.StoreId
                });
            }
            catch (Exception e)
            {
                return ResponseHelper.InternalServerError(e.Message);
            }
        }

        [HttpGet("GetAllShowcasesByStoreId/{storeId}")]
        public async Task<IActionResult> GetAllByStoreId(Guid storeId)
        {
            try
            {
                var result = (await _showcaseRepository.GetShowcasesByStoreId(storeId)).Select(x => new ShowcaseViewModel
                {
                    Id = x.Id,
                    ExclusiveCode = x.ExclusiveCode,
                    Name = x.Name,
                    StoreId = x.StoreId
                }).ToList();
                return ResponseHelper.Success(result);
            }
            catch (Exception e)
            {
                return ResponseHelper.InternalServerError(e.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] PostShowcaseViewModel vm)
        {
            try
            {
                var store = await _storeRepository.GetById(vm.StoreId);
                if (store == null)
                {
                    ResponseHelper.BadRequest("Loja não encontrada");
                }

                var exclusiveCode = await _showcaseRepository.GenerateExclusiveCode();
                var showcase = new Showcase()
                {
                    ExclusiveCode = exclusiveCode,
                    Name = vm.Name,
                    StoreId = vm.StoreId
                };

                var result = await _showcaseRepository.Insert(showcase);
                if (result > 0)
                {
                    ResponseHelper.Success();
                }

                return ResponseHelper.BadRequest();
            }
            catch (Exception e)
            {
                return ResponseHelper.InternalServerError(e.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> PutAsync([FromBody] PutShowcaseViewModel vm)
        {
            try
            {
                var showcase = await _showcaseRepository.GetById(vm.Id);
                if (showcase == null)
                {
                    return ResponseHelper.BadRequest("Nenhuma vitrine encontrado!");
                };

                showcase.Name = vm.Name;

                var result = await _showcaseRepository.Update(showcase);
                if (result > 0)
                {
                    return ResponseHelper.Success();
                }
                return ResponseHelper.BadRequest();
            }
            catch (Exception e)
            {
                return ResponseHelper.InternalServerError(e.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            try
            {
                var showcase = await _showcaseRepository.GetById(id);
                if (showcase == null)
                {
                    return ResponseHelper.BadRequest("Nenhuma vitrine encontrado!");
                };

                var style = _showcaseStyleRepository.Query().FirstOrDefault(x => !x.Deleted && x.ShowcaseId == id);
                if (style != null)
                {
                    var resultStyle = await _showcaseStyleRepository.Delete(style.Id);
                    if (resultStyle <= 0)
                    {
                        return ResponseHelper.BadRequest("Ocorreu um erro durante a exclusão do style da vitrine.");
                    }
                }

                var resultShowcase = await _showcaseRepository.Delete(id);
                if (resultShowcase <= 0)
                {
                    return ResponseHelper.BadRequest("Ocorreu um erro durante a exclusão da vitrine.");
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
