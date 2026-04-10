using Microsoft.AspNetCore.Mvc;
using KioskApi.Models;

namespace KioskApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private static List<Product> products = new List<Product>
        {
            new Product { Id = 1, Name = "아메리카노", Price = 1800},
            new Product { Id = 2, Name = "카페라떼"  , Price = 2500},
            new Product { Id = 3, Name = "카푸치노"  , Price = 3000}
        };

        [HttpGet]
        public IActionResult GetProducts()
        {
            return Ok(products);
        }

        [HttpPost]
        public IActionResult CreateProduct([FromBody] Product newProduct)
        {
            products.Add(newProduct);
            return Ok(newProduct);
        }
    }
}
