using Microsoft.AspNetCore.Mvc;
using zadanie7.Models;
using zadanie7.Services;

namespace zadanie7.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProductById(int id)
        {
            var productExists = await _productService.ExistsProductById(id);
            if (!productExists)
            {
                return NotFound();
            }

            var product = await _productService.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }

            return product;
        }
    }
}