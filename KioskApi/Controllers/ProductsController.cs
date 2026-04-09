using Microsoft.AspNetCore.Mvc;
using KioskApi.Models;

namespace KioskApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class ProductsController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetProducts()
        {
            var products = new List<Product>
            {
                new Product { Id = 1, Name = "아메리카노", Price = 1800},
                new Product { Id = 2, Name = "카페라떼"  , Price = 2500},
                new Product { Id = 3, Name = "카푸치노"  , Price = 3000}
            };
            return Ok(products);
        }

        [HttpGet("{id}")]
        public IActionResult GetProductById(int id)
        {
            var products = new List<Product>
            {
                new Product { Id = 1, Name = "아메리카노", Price = 1800},
                new Product { Id = 2, Name = "카페라떼"  , Price = 2500},
                new Product { Id = 3, Name = "카푸치노"  , Price = 3000}
            };

            var product = products.FirstOrDefault(p => p.Id == id);

            if (product == null)
                return NotFound();

            return Ok(product);
        }
    }
}
