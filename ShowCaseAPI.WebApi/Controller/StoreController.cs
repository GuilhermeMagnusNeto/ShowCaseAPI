using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using ShowCaseAPI.Domain.Entities;
using ShowCaseAPI.Repositories.Interface;
using ShowCaseAPI.Repositories.Repository;
using ShowCaseAPI.WebApi.Helper;
using ShowCaseAPI.WebApi.Model.Showcase;
using ShowCaseAPI.WebApi.Model.Store;
using ShowCaseAPI.WebApi.Model.User;
using System.Net;

namespace ShowCaseAPI.WebApi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class StoreController : ControllerBase
    {
        private readonly IStoreRepository _storeRepository;
        private readonly IUserRepository _userRepository;
        private readonly IStoreProductRepository _storeProductRepository;
        public StoreController(IStoreRepository storeRepository, IUserRepository userRepository, IStoreProductRepository storeProductRepository)
        {
            _storeRepository = storeRepository;
            _userRepository = userRepository;
            _storeProductRepository = storeProductRepository;
        }


        [HttpGet("GetById/{storeId}")]
        public async Task<IActionResult> GetByIdAsync(Guid storeId)
        {
            try
            {
                var result = await _storeRepository.GetById(storeId);
                if (result == null)
                {
                    return ResponseHelper.BadRequest("Nenhuma loja encontrado!");
                }
                return ResponseHelper.Success(new StoreViewModel
                {
                    Id = result.Id,
                    Name = result.Name,
                    StoreLogo = result.StoreLogo
                });
            }
            catch (Exception e)
            {
                return ResponseHelper.InternalServerError(e.Message);
            }
        }

        [HttpGet("GetAllStoresByUserId/{storeId}")]
        public async Task<IActionResult> GetAllStoresByUserId(Guid storeId)
        {
            try
            {
                var result = _storeRepository.Query().Where(x => !x.Deleted && x.UserId == storeId).Select(x => new StoreViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    StoreLogo = x.StoreLogo
                }).ToList();

                return ResponseHelper.Success(result);
            }
            catch (Exception e)
            {
                return ResponseHelper.InternalServerError(e.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] PostStoreViewModel vm)
        {
            try
            {
                var user = await _userRepository.GetById(vm.UserId);
                if (user == null)
                {
                    return ResponseHelper.BadRequest("Usuário não encontrado!");
                }
                var existeName = _storeRepository.Query().Where(x => !x.Deleted && x.UserId == user.Id).Any(x => x.Name.ToUpper() == vm.Name.ToUpper());
                if (existeName)
                {
                    return ResponseHelper.BadRequest("Você já tem uma loja com esse nome!");
                }


                //TODO:  StoreLogo = gerar url


                var store = new Store()
                {
                    Name = vm.Name,
                    //StoreLogo = 
                    UserId = user.Id
                };
                var result = await _storeRepository.Insert(store);
                if (result > 0)
                {
                    return ResponseHelper.Success(new StoreViewModel
                    {
                        Id = store.Id,
                        Name = store.Name,
                        StoreLogo = store.StoreLogo
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
        public async Task<IActionResult> PutAsync([FromBody] PutStoreViewModel vm)
        {
            try
            {
                var store = await _storeRepository.GetById(vm.storeId);
                if (store == null)
                {
                    return ResponseHelper.BadRequest("Nenhuma loja encontrado!");
                };
                var user = await _userRepository.GetById(store.UserId);
                if (user == null)
                {
                    return ResponseHelper.BadRequest("Usuário não encontrado!");
                }
                var existeName = _storeRepository.Query().Where(x => !x.Deleted && x.UserId == user.Id && x.Id != store.Id).Any(x => x.Name.ToUpper() == vm.Name.ToUpper());
                if (existeName)
                {
                    return ResponseHelper.BadRequest("Você já tem uma loja com esse nome!");
                }


                //TODO:  StoreLogo = gerar url


                store.Name = vm.Name;
                //store.StoreLogo =

                var result = await _storeRepository.Update(store);
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

        [HttpDelete("{storeId}")]
        public async Task<IActionResult> DeleteAsync(Guid storeId)
        {
            try
            {
                var store = await _storeRepository.GetById(storeId);
                if (store == null)
                {
                    return ResponseHelper.BadRequest("Nenhuma loja encontrado!");
                };

                var products = await _storeProductRepository.GetProductsByStoreId(store.Id);
                foreach (var product in products)
                {
                    var productResult = await _storeProductRepository.Delete(product.Id);
                    if (productResult <= 0)
                    {
                        return ResponseHelper.BadRequest("Ocorreu um erro durante a exclusão dos produtos da loja.");
                    }
                }

                var resultStore = await _storeRepository.Delete(storeId);
                if (resultStore <= 0)
                {
                    return ResponseHelper.BadRequest("Ocorreu um erro durante a exclusão da loja.");
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
