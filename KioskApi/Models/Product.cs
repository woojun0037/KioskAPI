using System.ComponentModel.DataAnnotations;

namespace KioskApi.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "상품명은 필수입니다.")]
        [StringLength(20, ErrorMessage = "상품명은 20자 이하로 입력해주세요.")]
        public string Name { get; set; } = "";

        [Range(1, 100000, ErrorMessage = "가격은 1원 이상이어야 합니다.")] 
        public int Price { get; set; }

        public string Category { get; set; } = "";
    }
}
