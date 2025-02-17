

using System.ComponentModel.DataAnnotations;

namespace Core.DTOs
{
    public class CreateDeliveryMethodDto
    {
        [MaxLength(50, ErrorMessage = "ShortName must be less than 50 character")]
        [Required]
        public string ShortName { get; set; } = string.Empty;

        [MaxLength(50, ErrorMessage = "DeliveryTime must be less than 50 character")]
        [Required]
        public string DeliveryTime { get; set; } = string.Empty;

        [MaxLength(50, ErrorMessage = "Description must be less than 50 character")]
        [Required]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "Price is required")]
        [Range(0, double.MaxValue, ErrorMessage = "Price must be at least 0")]
        public decimal? Price { get; set; }
    }
    public class DeliveryMethodDto : CreateDeliveryMethodDto
    {
        public int Id { get; set; }     
    }
}
