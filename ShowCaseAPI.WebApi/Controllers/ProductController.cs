using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShowCaseAPI.Domain.Entities;
using ShowCaseAPI.Repositories.Interface;
using ShowCaseAPI.WebApi.Model.Product;
using ShowCaseAPI.WebApi.Model.User;
using System.Net;

namespace ShowCaseAPI.WebApi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            try
            {
                var result = await _productRepository.GetById(id);
                if (result == null)
                {
                    return NotFound("Nenhum produto encontrado!");
                }
                return Ok(result);
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, e.Message);
            }
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                var result = (await _productRepository.GetAll()).ToList();
                return Ok(result);
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, e.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync(PostProductViewModel vm)
        {
            try
            {
                var product = new Product()
                {
                    Name = vm.Name,
                    Value = vm.Value
                };
                var result = await _productRepository.Insert(product);
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

        [HttpPut]
        public async Task<IActionResult> PutAsync(PutProductViewModel vm)
        {
            try
            {
                var product = await _productRepository.GetById(vm.Id);
                if (product == null)
                {
                    return BadRequest("Nenhum produto encontrado!");
                };

                product.Name = vm.Name;
                product.Value = vm.Value;

                var result = await _productRepository.Update(product);
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            try
            {
                var product = await _productRepository.GetById(id);
                if (product == null)
                {
                    return BadRequest("Nenhum produto encontrado!");
                };

                var result = await _productRepository.Delete(id);
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
