

using System.ComponentModel.DataAnnotations;

namespace Core.DTOs
{
    public class AddressDto
    {
        [Required]
        [MaxLength(200)]
        public string Line { get; set; } = string.Empty;
        [Required]
        [MaxLength(50)]
        public string City { get; set; } = string.Empty;
        [Required]
        [MaxLength(50)]
        public string State { get; set; } = string.Empty;
        [Required]
        [MaxLength(10)]
        public string Zipcode { get; set; } = string.Empty;
        [Required]
        [MaxLength(50)]
        public string Country { get; set; } = string.Empty;
    }
}
