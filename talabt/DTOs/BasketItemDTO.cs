using System.ComponentModel.DataAnnotations;

namespace talabtAPIs.DTOs
{
    public class BasketItemDTO
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string ProductName { get; set; }
        [Required]
        public string PictureUrl { get; set; }
        [Required]
        public string Brand { get; set; }
        [Required]
        public string Category { get; set; }
        [Required]
        [Range(0.1, double.MaxValue, ErrorMessage = "Quantity must be one item at least")]
        public decimal Price { get; set; }
        [Required]
        [Range(1,int.MaxValue,ErrorMessage ="Price can not be zero")]
        public int Quantity { get; set; }
    }
}
