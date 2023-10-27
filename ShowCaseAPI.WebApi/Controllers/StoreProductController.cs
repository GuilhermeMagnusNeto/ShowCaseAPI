using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShowCaseAPI.Domain.Entities;
using ShowCaseAPI.Repositories.Interface;
using ShowCaseAPI.WebApi.Model.Product;
using ShowCaseAPI.WebApi.Model.Store;
using ShowCaseAPI.WebApi.Model.User;
using System.Net;

namespace ShowCaseAPI.WebApi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class StoreProductController : ControllerBase
    {
        private readonly IStoreRepository _storeRepository;
        private readonly IStoreProductRepository _storeProductRepository;
        public StoreProductController(IStoreProductRepository storeProductRepository, IStoreRepository storeRepository)
        {
            _storeProductRepository = storeProductRepository;
            _storeRepository = storeRepository;
        }


        [HttpGet("GetProductById/{id}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            try
            {
                var result = await _storeProductRepository.GetById(id);
                if (result == null)
                {
                    return NotFound("Nenhum produto encontrado!");
                }

                return Ok(new ProductViewModel
                {
                    Id = result.Id,
                    StoreId = result.StoreId,
                    Name = result.Name,
                    Value = result.Value,
                    SKU = result.SKU
                });
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, e.Message);
            }
        }

        [HttpGet("GetAllProductsByStoreId/{storeId}")]
        public async Task<IActionResult> GetAllByStoreId(Guid storeId)
        {
            try
            {
                var result = (await _storeProductRepository.GetProductsByStoreId(storeId)).Select(x => new ProductViewModel
                {
                    Id = x.Id,
                    StoreId = x.StoreId,
                    Name = x.Name,
                    Value = x.Value,
                    SKU = x.SKU
                }).ToList();
                return Ok(result);
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, e.Message);
            }
        }

        [HttpPost("CreateProduct")]
        public async Task<IActionResult> PostAsync([FromBody] PostProductViewModel vm)
        {
            try
            {
                var store = await _storeRepository.GetById(vm.StoreId);
                if (store == null)
                {
                    BadRequest("Loja não encontrada");
                }

                if (vm.SKU != null)
                {
                    var products = await _storeProductRepository.GetProductsByStoreId(vm.StoreId);
                    var existSKU = products.Any(x => x.SKU != null && x.SKU.ToUpper() == vm.SKU.ToUpper());
                    if (existSKU)
                    {
                        return BadRequest("Você já tem um produto com esse código SKU registrado para esta loja!");
                    }

                }

                var product = new StoreProduct()
                {
                    StoreId = vm.StoreId,
                    Name = vm.Name,
                    Value = vm.Value,
                    SKU = vm.SKU
                };

                var result = await _storeProductRepository.Insert(product);
                if (result > 0)
                {
                    return Ok("Producto criado com sucesso!");
                }
                return BadRequest("Ocorreu um erro durante a criação do produto.");
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, e.Message);
            }
        }

        [HttpPut("EditProduct")]
        public async Task<IActionResult> PutAsync([FromBody] PutProductViewModel vm)
        {
            try
            {
                var product = await _storeProductRepository.GetById(vm.Id);
                if (product == null)
                {
                    return BadRequest("Nenhum produto encontrado!");
                };

                if (vm.SKU != null)
                {
                    var products = await _storeProductRepository.GetProductsByStoreId(product.StoreId);
                    var existSKU = products.Where(x => x.Id != product.Id).Any(x => x.SKU != null && x.SKU.ToUpper() == vm.SKU.ToUpper());
                    if (existSKU)
                    {
                        return BadRequest("Você já tem um produto com esse código SKU registrado para esta loja!");
                    }
                }

                product.Name = vm.Name;
                product.SKU = vm.SKU;
                product.Value = vm.Value;

                var result = await _storeProductRepository.Update(product);
                if (result > 0)
                {
                    return Ok("Produto atualizado com sucesso!");
                }
                return BadRequest("Ocorreu um erro durante a atualização do produto.");
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, e.Message);
            }
        }

        [HttpDelete("DeleteProduct/{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            try
            {
                var product = await _storeProductRepository.GetById(id);
                if (product == null)
                {
                    return BadRequest("Nenhum produto encontrado!");
                };

                var result = await _storeProductRepository.Delete(id);
                if (result > 0)
                {
                    return Ok("Produto excluido com sucesso!");
                }

                return BadRequest("Ocorreu um erro durante a exclusão do produto.");
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, e.Message);
            }
        }
    }
}
