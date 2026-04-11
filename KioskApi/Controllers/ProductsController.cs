using Microsoft.AspNetCore.Mvc;
using KioskApi.Models;
using System.Net;

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

        [HttpGet("{id}")]
        public IActionResult GetProductById(int id)
        {
            var product = products.FirstOrDefault(p => p.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [HttpPost]
        public IActionResult CreateProduct([FromBody] Product newProduct)
        {
            newProduct.Id = GenerateNextProductId();

            products.Add(newProduct);

            return CreatedAtAction(nameof(GetProductById), new { id = newProduct.Id }, newProduct);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateProduct(int id, [FromBody] Product updateProduct)
        {
            var product = FindProductById(id);

            if (product == null)
            {
                return NotFound();
            }

            UpdateProductInfo(product, updateProduct);

            return Ok(product);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            var product = FindProductById(id);

            if (product == null)
            {
                return NotFound();
            }

            RemoveProduct(product);

            return NoContent();
        }

        //상품 ID  생성 로직
        private int GenerateNextProductId()
        {
            return products.Any() ? products.Max(p => p.Id) + 1 : 1;
        }
        
        //기존 상품 객체의 정보(Name, Price)를 수정
        private void UpdateProductInfo(Product product, Product updateProduct)
        {
            product.Name  = updateProduct.Name;
            product.Price = updateProduct.Price;
        }

        //id로 상품 하나 찾기
        private Product? FindProductById(int id)
        {
            return products.FirstOrDefault(p => p.Id == id);
        }

        //id로 상품 하나 찾고 삭제
        private void RemoveProduct(Product product)
        {
            products.Remove(product);
        }
    }
}
