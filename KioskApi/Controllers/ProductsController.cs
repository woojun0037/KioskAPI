using System.Net;
using System.Diagnostics;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using KioskApi.Models;
using KioskApi.Dtos;
using KioskApi.Data;
using KioskApi.Services;

namespace KioskApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _ProductService;

        public ProductsController(IProductService productService)
        {
            _ProductService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var respone = await _ProductService.GetProductsAsync();
            return Ok(respone);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await _ProductService.GetCreateProductByIdAsync(id);

            if(product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductRequest request)
        {
            var createdProduct = await _ProductService.CreateProductAsync(request);

            return CreatedAtAction(nameof(GetProductById), new { id = createdProduct.Id}, createdProduct);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] UpdateProductRequest request)
        {
            var updateProduct = await _ProductService.UpdateProductAsync(id, request);

            if (updateProduct == null)
            {
                return NotFound();
            }
            return Ok(updateProduct);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductAsync(int id)
        {
            var isDelete = await _ProductService.DeleteProductAsync(id);

            if (!isDelete)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
