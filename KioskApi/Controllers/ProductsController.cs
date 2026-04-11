using System.Net;
using System.Diagnostics;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using KioskApi.Models;
using KioskApi.Dtos;
using KioskApi.Data;

namespace KioskApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProductsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var productList = await _context.Products.ToListAsync();
            var response = ToProductResponseList(productList);
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p=> p.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(ToProductResponse(product));
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductRequest request)
        {
            var newProduct = new Product
            {
                Name = request.Name,
                Price = request.Price
            };

            _context.Products.Add(newProduct);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProductById), new { id = newProduct.Id }, ToProductResponse(newProduct));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] UpdateProductRequest request)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            UpdateProductInfo(product, request);

            await _context.SaveChangesAsync();

            return Ok(ToProductResponse(product));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
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
            return product.Select(product => ToProductResponse(product)).ToList();
        }
    }
}
