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
        private readonly IShowcaseProductRepository _showcaseProductRepository;
        private readonly IConfiguration _configuration;
        public ShowcaseController(IShowcaseRepository showcaseRepository, IStoreRepository storeRepository, IShowcaseStyleRepository showcaseStyleRepository, IShowcaseProductRepository showcaseProductRepository, IConfiguration configuration)
        {
            _showcaseRepository = showcaseRepository;
            _storeRepository = storeRepository;
            _showcaseStyleRepository = showcaseStyleRepository;
            _showcaseProductRepository = showcaseProductRepository;
            _configuration = configuration;
        }


        [HttpGet("GetById/{showcaseId}")]
        public async Task<IActionResult> GetByIdAsync(Guid showcaseId)
        {
            try
            {
                var result = await _showcaseRepository.GetById(showcaseId);
                if (result == null)
                {
                    return ResponseHelper.BadRequest("Nenhuma vitrine encontrada!");
                }

                var uniqueLink = Path.Combine(_configuration.GetSection("GlobalShowcase:Url").Value, result.ExclusiveCode);
                return ResponseHelper.Success(new ShowcaseViewModel
                {
                    Id = result.Id,
                    UniqueLink = uniqueLink.Replace("\\", "/"),
                    Name = result.Name,
                    StoreId = result.StoreId
                });
            }
            catch (Exception e)
            {
                return ResponseHelper.InternalServerError(e.Message);
            }
        }

        [HttpGet("GetByExclusiveCode/{code}")]
        public async Task<IActionResult> GetByIdAsync(string code)
        {
            try
            {
                var result = _showcaseRepository.Query().FirstOrDefault(x => x.ExclusiveCode == code);
                if (result == null)
                {
                    return ResponseHelper.BadRequest("Nenhum produto encontrado!");
                }

                var uniqueLink = Path.Combine(_configuration.GetSection("GlobalShowcase:Url").Value, result.ExclusiveCode);
                return ResponseHelper.Success(new ShowcaseViewModel
                {
                    Id = result.Id,
                    UniqueLink = uniqueLink.Replace("\\", "/"),
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
                    UniqueLink = Path.Combine(_configuration.GetSection("GlobalShowcase:Url").Value, x.ExclusiveCode).Replace("\\", "/"),
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
                    return ResponseHelper.BadRequest("Loja não encontrada");
                }

                var existeName = _showcaseRepository.Query().Where(x => !x.Deleted && x.StoreId == store.Id).Any(x => x.Name.ToUpper() == vm.Name.ToUpper());
                if (existeName)
                {
                    return ResponseHelper.BadRequest("Você já tem uma vitrine com esse nome!");
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
                    return ResponseHelper.Success(new ShowcaseViewModel
                    {
                        Id = showcase.Id,
                        UniqueLink = Path.Combine(_configuration.GetSection("GlobalShowcase:Url").Value, showcase.ExclusiveCode).Replace("\\", "/"),
                        Name = showcase.Name,
                        StoreId = showcase.StoreId
                    });
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
                var showcase = await _showcaseRepository.GetById(vm.ShowcaseId);
                if (showcase == null)
                {
                    return ResponseHelper.BadRequest("Nenhuma vitrine encontrado!");
                };

                var existeName = _showcaseRepository.Query().Where(x => !x.Deleted && x.StoreId == showcase.StoreId && x.Id != showcase.Id).Any(x => x.Name.ToUpper() == vm.Name.ToUpper());
                if (existeName)
                {
                    return ResponseHelper.BadRequest("Você já tem uma vitrine com esse nome!");
                }
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

        [HttpDelete("{showcaseId}")]
        public async Task<IActionResult> DeleteAsync(Guid showcaseId)
        {
            try
            {
                var showcase = await _showcaseRepository.GetById(showcaseId);
                if (showcase == null)
                {
                    return ResponseHelper.BadRequest("Nenhuma vitrine encontrado!");
                };

                var style = _showcaseStyleRepository.Query().FirstOrDefault(x => !x.Deleted && x.ShowcaseId == showcaseId);
                if (style != null)
                {
                    var resultStyle = await _showcaseStyleRepository.Delete(style.Id);
                    if (resultStyle <= 0)
                    {
                        return ResponseHelper.BadRequest("Ocorreu um erro durante a exclusão do style da vitrine.");
                    }
                }

                var showcaseProducts = _showcaseProductRepository.Query().Where(x => !x.Deleted && x.ShowcaseId == showcaseId).ToList();
                foreach (var showcaseProduct in showcaseProducts)
                {
                    var removed = await _showcaseProductRepository.Delete(showcaseProduct.Id);
                    if (removed <= 0)
                    {
                        return ResponseHelper.BadRequest("Ocorreu um erro durante a exclusão dos produtos antigos.");
                    }
                }

                var resultShowcase = await _showcaseRepository.Delete(showcaseId);
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
