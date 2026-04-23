using System.ComponentModel.DataAnnotations;

namespace KioskApi.Dtos
{
    public class UpdateProductRequest
    {
        [Required(ErrorMessage = "상품명은 필수입니다.")]
        [StringLength(100, ErrorMessage = "상품명은 1자 이상 100자 이하로 입력해주세요.")]
        public string Name { get; set; } = "";

        [Range(1, 1000000, ErrorMessage = "가격은 1원 이상 1000000 이하로 입력해주세요.")] 
        public int Price { get; set; }
    }
}
