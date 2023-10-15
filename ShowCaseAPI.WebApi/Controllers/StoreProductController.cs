using Microsoft.AspNetCore.Mvc;
using ShowCaseAPI.Domain.Entities;
using ShowCaseAPI.Repositories.Interface;
using ShowCaseAPI.WebApi.Model.Product;
using System.Net;

namespace ShowCaseAPI.WebApi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class StoreProductController : ControllerBase
    {
        private readonly IStoreProductRepository _storeProductRepository;
        public StoreProductController(IStoreProductRepository storeProductRepository)
        {
            _storeProductRepository = storeProductRepository;
        }

        /// <summary>
        /// Pegar todos produtos da loja
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("GetAll/{storeId}")]
        public async Task<IActionResult> GetAllProductsAysinc(Guid storeId)
        {
            try
            {
                var result = await _storeProductRepository.GetAll();
                var products = result.Where(x => x.StoreId == storeId).ToList().Select(x => x.Product);
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
    }
}
