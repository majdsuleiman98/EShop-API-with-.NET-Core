
using System.ComponentModel.DataAnnotations;

namespace Core.DTOs
{
    public class CreateCategoryDto
    {
        [MaxLength(50, ErrorMessage = "Name must be less than 50 character")]
        [Required]
        public string Name { get; set; } = string.Empty;
    }
    public class CategoryDto : CreateBrandDto
    {
        public int Id { get; set; }
        
    }
}
