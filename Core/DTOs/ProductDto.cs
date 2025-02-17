
using System.ComponentModel.DataAnnotations;

namespace Core.DTOs
{
    public class CreateProductDto
    {
        [MaxLength(50, ErrorMessage = "Name must be less than 50 character")]
        [Required]
        public string Name { get; set; } = string.Empty;
        [MaxLength(200, ErrorMessage = "Description must be less than 200 character")]
        [Required]
        public string Description { get; set; } = string.Empty;
        [Required(ErrorMessage = "Price is required")]
        [Range(0.1, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
        public required decimal Price { get; set; }
        public required string PictureUrl { get; set; }
        [Required(ErrorMessage = "Quantity is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1")]
        public required int QuantityInStock { get; set; }
        [Required(ErrorMessage = "CategoryId is required")]
        public int CategoryId { get; set; }
        [Required(ErrorMessage = "BrandId is required")]
        public int BrandId { get; set; }
    }

    public class ProductDto : CreateProductDto
    {
        public int Id { get; set; }       
        public string? CategoryName { get; set; }
        public string? BrandName { get; set; }
    }
}
