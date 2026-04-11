using System.Net;
using Microsoft.AspNetCore.Mvc;
using KioskApi.Models;
using KioskApi.Dtos;
using System.Diagnostics;

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
            var response = ToProductResponseList(products);
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

            return Ok(ToProductResponse(product));
        }

        [HttpPost]
        public IActionResult CreateProduct([FromBody] CreateProductRequest request)
        {
            var newProduct = new Product
            {
                Id = GenerateNextProductId(),
                Name = request.Name,
                Price = request.Price
            };

            products.Add(newProduct);

            return CreatedAtAction(nameof(GetProductById), new { id = newProduct.Id }, ToProductResponse(newProduct));
        }

        [HttpPut("{id}")]
        public IActionResult UpdateProduct(int id, [FromBody] UpdateProductRequest request)
        {
            var product = FindProductById(id);

            if (product == null)
            {
                return NotFound();
            }

            UpdateProductInfo(product, request);

            return Ok(ToProductResponse(product));
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
        private void UpdateProductInfo(Product product, UpdateProductRequest request)
        {
            product.Name  = request.Name;
            product.Price = request.Price;
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

        /// <summary>
        /// Product 엔티티를 클라이언트에 반환할 ProdcutResponse DTO로 변환합니다.
        /// </summary>
        /// <param name="product">응답 DTO로 변환할 상품 엔티티</param>
        /// <returns>상품 응답 DTO</returns>
        private ProductResponse ToProductResponse(Product product)
        {
            return new ProductResponse
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price
            };
        }

        /// <summary>
        /// Product 엔티티 목록을 클라이언트에 변환할 ProductResponse DTO 목록으로 변환합니다.
        /// </summary>
        /// <param name="product">응답 DTO 목록으로 변환할 상품 엔티티 목록</param>
        /// <returns>상품 응답 DTO 목록</returns>
        private List<ProductResponse> ToProductResponseList(List<Product> product)
        {
            return products.Select(product => ToProductResponse(product)).ToList();
        }
    }
}
