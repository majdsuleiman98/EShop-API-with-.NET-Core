
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Core.Entities
{
    public class Product: BaseEntity
    {
        public required string Name { get; set; }
        public required string Description { get; set; }
        [Range(0.1, double.MaxValue)]
        public decimal Price { get; set; }
        public required string PictureUrl { get; set; }
        [Range(1, int.MaxValue)]
        public int QuantityInStock { get; set; }
        [ForeignKey(nameof(Category))]
        public int CategoryId { get; set; }
        public Category Category { get; set; } = null!;
        [ForeignKey(nameof(Brand))]
        public int BrandId { get; set; }
        public Brand Brand { get; set; } = null!;
    }
}
