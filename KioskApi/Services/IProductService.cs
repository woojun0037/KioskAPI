using KioskApi.Dtos;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace KioskApi.Services
{
    /// <summary>
    /// 상품 관련 비즈니스 로직을 제공하는 서비스 인터페이스 입니다.
    /// </summary>
    public interface IProductService
    {
        /// <summary>
        /// 전체 상품 목록을 조회합니다.
        /// </summary>
        /// <returns>상품 응답 DTO 목록</returns>
        Task<List<ProductResponse>> GetProductsAsync();

        /// <summary>
        /// 전달 받은 ID와 일치 하는 상품을 조회합니다.
        /// </summary>
        /// <param name="id">조회할 상품 ID</param>
        /// <returns>조회된 상품 응답 DTO, 없으면 null</returns>
        Task<ProductResponse?> GetCreateProductByIdAsync(int id);

        /// <summary>
        /// 새로운 상품을 생성합니다.
        /// </summary>
        /// <param name="request">상품 생성을 요청 DTO</param>
        /// <returns>생성된 상품 응답 DTO</returns>
        Task<ProductResponse> CreateProductAsync(CreateProductRequest request);

        /// <summary>
        /// 전달 받은 ID와 일치하는 상품 정보를 수집합니다.
        /// </summary>
        /// <param name="id">수정할 상품</param>
        /// <param name="request">수정할 상품 정보가 담긴 요청 DTO</param>
        /// <returns>수정된 상품 응답 DTO, 없으면 null</returns>
        Task<ProductResponse?> UpdateProductAsync(int id, UpdateProductRequest request);

        /// <summary>
        /// 전달 받은 ID와 일치하는 상품을 삭제 합니다.
        /// </summary>
        /// <param name="id">삭제할 상품</param>
        /// <returns>삭제 성공 여부</returns>
        Task<bool> DeleteProductAsync(int id);
    }
}
