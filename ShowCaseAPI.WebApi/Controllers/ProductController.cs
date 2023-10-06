using Microsoft.AspNetCore.Mvc;
using ShowCaseAPI.Domain.Entities;
using ShowCaseAPI.Repositories.Interface;
using ShowCaseAPI.WebApi.Model.Product;
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
        public IActionResult GetById(Guid id)
        {
            try
            {
                var result = _productRepository.GetById(id);
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
        public IActionResult GetAll()
        {
            try
            {
                var result = _productRepository.GetAll();
                return Ok(result);
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, e.Message);
            }
        }

        [HttpPost]
        public IActionResult Post(PostProductViewModel vm)
        {
            try
            {
                var product = new Product()
                {
                    Name = vm.Name,
                    Value = vm.Value
                };
                var result = _productRepository.Insert(product);
                if (result > 0)
                {
                    return Ok(result);
                }
                return BadRequest("Ocorreu um erro durante a criação do produto.");
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, e.Message);
            }
        }

        [HttpPut]
        public IActionResult Put(PutProductViewModel vm)
        {
            try
            {
                var product = _productRepository.GetById(vm.Id);
                if (product == null)
                {
                    return BadRequest("Nenhum produto encontrado!");
                };

                product.Name = vm.Name;
                product.Value = vm.Value;

                var result = _productRepository.Update(product);
                if (result > 0)
                {
                    return Ok(result);
                }
                return BadRequest("Ocorreu um erro durante a atualização do produto.");
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, e.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                var product = _productRepository.GetById(id);
                if (product == null)
                {
                    return BadRequest("Nenhum produto encontrado!");
                };

                var result = _productRepository.Delete(product.Id);
                if (result > 0)
                {
                    return Ok(result);
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
