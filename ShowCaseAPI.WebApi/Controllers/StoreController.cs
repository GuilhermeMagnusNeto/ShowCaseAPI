using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using ShowCaseAPI.Domain.Entities;
using ShowCaseAPI.Repositories.Interface;
using ShowCaseAPI.Repositories.Repository;
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


        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            try
            {
                var result = await _storeRepository.GetById(id);
                if (result == null)
                {
                    return NotFound("Nenhuma loja encontrado!");
                }
                return Ok(result);
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, e.Message);
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
                    return BadRequest("Usuário não encontrado!");
                }
                var existeName = _storeRepository.Query().Where(x => x.UserId == user.Id).Any(x => x.Name.ToUpper() == vm.Name.ToUpper());
                if (existeName)
                {
                    return BadRequest("Você já tem uma loja com esse nome!");
                }

                var store = new Store()
                {
                    Name = vm.Name,
                    //TODO:  StoreLogo = gerar url
                    UserId = user.Id
                };
                var result = await _storeRepository.Insert(store);
                if (result > 0)
                {
                    return Ok("Loja criada com sucesso!");
                }
                return BadRequest("Ocorreu um erro durante a criação da loja!.");
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, e.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> PutAsync(PutStoreViewModel vm)
        {
            try
            {
                var store = await _storeRepository.GetById(vm.Id);
                if (store == null)
                {
                    return BadRequest("Nenhuma loja encontrado!");
                };

                var user = await _userRepository.GetById(store.UserId);
                if (user == null)
                {
                    return BadRequest("Usuário não encontrado!");
                }
                var existeName = _storeRepository.Query().Where(x => x.UserId == user.Id).Any(x => x.Name.ToUpper() == vm.Name.ToUpper());
                if (existeName)
                {
                    return BadRequest("Você já tem uma loja com esse nome!");
                }

                store.Name = vm.Name;
                //TODO: store.StoreLogo =

                var result = await _storeRepository.Update(store);
                if (result > 0)
                {
                    return Ok("Loja atualizado com sucesso!");
                }
                return BadRequest("Ocorreu um erro durante a atualização da loja.");
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, e.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            try
            {
                var store = await _storeRepository.GetById(id);
                if (store == null)
                {
                    return BadRequest("Nenhuma loja encontrado!");
                };

                var products = await _storeProductRepository.GetProductsByStoreId(store.Id);
                foreach (var product in products)
                {
                    var productResult = await _storeProductRepository.Delete(product.Id);
                    if (productResult <= 0)
                    {
                        return BadRequest("Ocorreu um erro durante a exclusão dos produtos da loja.");
                    }
                }

                var resultStore = await _storeRepository.Delete(id);
                if (resultStore <= 0)
                {
                    return BadRequest("Ocorreu um erro durante a exclusão da loja.");
                }

                return Ok("Loja excluida com sucesso!");
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, e.Message);
            }
        }
    }
}
