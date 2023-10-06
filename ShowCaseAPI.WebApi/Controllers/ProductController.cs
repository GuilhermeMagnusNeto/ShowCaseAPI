using Microsoft.AspNetCore.Mvc;
using ShowCaseAPI.Repositories.Interface;

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

        //[HttpGet]
        //public async IActionResult Teste() 
        //{ 
        //    await _productRepository.InsertAsync();
        //}
    }
}
