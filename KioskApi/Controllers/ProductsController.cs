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
            return Ok(response);
        }

        [HttpGet("{id}")]
        public IActionResult GetProductById(int id)
        {
            var product = FindProductById(id);

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

        /// <summary>
        /// 현재 상품 목록을 기준으로 다음 상품 ID를 생성합니다.
        /// 기존 상품이 비어 있으면 최대 ID에 1을 더한 값을 반환하고,
        /// 목록이 비어 있으면 1을 반환합니다.
        /// </summary>
        /// <returns>새 상품에 할당 할 ID 값</returns>
        private int GenerateNextProductId()
        {
            /// <summary>
            /// 전달 받은 ID와 일치하는 상품 엔티티를 조회 합니다.
            /// </summary>
            /// <param name="id">조회 할 상품 ID</param>
            /// <reutrns>조회된 상품 엔티티, 없으면 null</reutrns>
            return products.Any() ? products.Max(p => p.Id) + 1 : 1;
        }

        /// <summary>
        /// 기존 상품 엔티티의 이름과 가격 정보를 수정합니다.
        /// </summary>
        /// <param name="product"> 수정 할 기존 상품 엔티티</param>
        /// <param name="request"> 수정 할 상품 정보가 담긴 요청 DTO</param>
        private void UpdateProductInfo(Product product, UpdateProductRequest request)
        {
            product.Name  = request.Name;
            product.Price = request.Price;
        }

        /// <summary>
        /// 전달 받은 ID와 일치하는 상품 엔티티를 조회합니다.
        /// </summary>
        /// <param name="id">조회 할 상품 ID</param>
        /// <returns>조회된 상품 엔티티, 없으면 null</returns>
        private Product? FindProductById(int id)
        {
            return products.FirstOrDefault(p => p.Id == id);
        }

        /// <summary>
        /// 전달 받은 상품 엔티티를 목록에서 제거합니다
        /// </summary>
        /// <param name="product">삭제 할 상품 엔티티</param>
        private void RemoveProduct(Product product)
        {
            products.Remove(product);
        }

        /// <summary>
        /// 상품 엔티티를 응답 DTO로 변환합니다.
        /// </summary>
        /// <param name="product">변환할 상품 엔티티</param>
        /// <returns>상품 응답 DTO</returns>
        private ProductResponse ToProductResponse(Product product)
        {
            return new ProductResponse
            {
                Id    = product.Id,
                Name  = product.Name,
                Price = product.Price
            };
        }

        /// <summary>
        /// 상품 엔티티 List를 응답 DTO List로 변환합니다.
        /// </summary>
        /// <param name="product">변환 할 상품 엔티티 목록</param>
        /// <returns>상품 응답 DTO 목록</returns>
        private List<ProductResponse> ToProductResponseList(List<Product> product)
        {
            return products.Select(product => ToProductResponse(product)).ToList();
        }
    }
}
