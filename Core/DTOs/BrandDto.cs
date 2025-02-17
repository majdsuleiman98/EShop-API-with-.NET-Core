
using System.ComponentModel.DataAnnotations;

namespace Core.DTOs
{
    public class CreateBrandDto
    {
        [MaxLength(50, ErrorMessage = "Name must be less than 50 character")]
        [Required]
        public string Name { get; set; } = string.Empty;
    }
    public class BrandDto : CreateBrandDto
    {
        public int Id { get; set; }
        
    }
}
