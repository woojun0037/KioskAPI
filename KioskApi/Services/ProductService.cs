using KioskApi.Data;
using KioskApi.Dtos;
using KioskApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace KioskApi.Services
{
    /// <summary>
    /// 상품관련 비즈니스 로직을 처리하는 서비스 클래스입니다.
    /// </summary>
    public class ProductService : IProductService
    {
        private readonly AppDbContext _context;

        public ProductService(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// 전체 상품 목록을 조회합니다.
        /// </summary>
        /// <returns>상품 응답 목록 DTO목록</returns>
        public async Task<List<ProductResponse>> GetProductsAsync()
        {
            var product = await _context.Products.ToListAsync();
            return ToProductResponseList(product);
        }

        /// <summary>
        /// 전달 받은 ID와 일치하는 상품을 조회합니다.
        /// </summary>
        /// <param name="id">조회 할 상품 ID</param>
        /// <returns>조회된 상품 응답 DTO, 없으면 null</returns>
        public async Task<ProductResponse?> GetCreateProductByIdAsync(int id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
           
            if(product == null)
            {
                return null;
            }

            return ToProductResponse(product);
        }

        /// <summary>
        /// 새로운 상품을 생성합니다.
        /// </summary>
        /// <param name="request">상품 생성 요청 DTO</param>
        /// <returns>생성된 상품을 응답 DTO</returns>
        public async Task<ProductResponse> CreateProductAsync(CreateProductRequest request)
        {
            var newProduct = new Product
            {
                Name  = request.Name,
                Price = request.Price
            };

            _context.Products.Add(newProduct);
            await _context.SaveChangesAsync();

            return ToProductResponse(newProduct);
        }

        /// <summary>
        /// 전달 받은 ID와 일치하는 상품 정보를 수집합니다.
        /// </summary>
        /// <param name="id">수정 할 상품 ID</param>
        /// <param name="request">수정 할 상품 정보가 담긴 요청 DTO</param>
        /// <returns>수정된 상품 응답 DTO, 없으면 null</returns>
        public async Task<ProductResponse?> UpdateProductAsync(int id, UpdateProductRequest request)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);

            if(product == null)
            {
                return null;
            }

            product.Name  = request.Name;
            product.Price = request.Price;

            await _context.SaveChangesAsync();

            return ToProductResponse(product);
        }

        /// <summary>
        /// 전달 받은 ID와 일치 하는 상품을 삭제합니다.
        /// </summary>
        /// <param name="id">삭제할 상품 ID</param>
        /// <returns>삭제 성공 여부</returns>
        public async Task<bool> DeleteProductAsync(int id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);

            if(product == null)
            {
                return false;
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync(true);

            return true;
        }

        /// <summary>
        /// 기존 상품 엔티티의 이름과 가격 정보를 수집합니다.
        /// </summary>
        /// <param name="product">수정 할 기존 상품 엔티티</param>
        /// <param name="request">수정 할 상품 정보가 담긴 요청 DTO</param>
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
        /// 상품 엔티티 목록을 응답 DTO 목록으로 변환합니다.
        /// </summary>
        /// <param name="products">변환 할 상품 엔티티 목록</param>
        /// <returns>상품 응답 DTO 목록</returns>
        private List<ProductResponse> ToProductResponseList(List<Product> products)
        {
            return products.Select(product => ToProductResponse(product)).ToList();
        }
    }
}
