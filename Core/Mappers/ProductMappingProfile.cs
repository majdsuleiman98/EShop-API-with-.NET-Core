
using Core.DTOs;
using Core.Entities;

namespace Core.Mappers
{
    public static class ProductMappingProfile
    {
        public static ProductDto ToDto(this Product product)
        {
            return new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                PictureUrl = product.PictureUrl,
                QuantityInStock = product.QuantityInStock,
                CategoryId = product.CategoryId,
                BrandId = product.BrandId,
                CategoryName = product.Category?.Name,
                BrandName = product.Brand?.Name,
            };
        }

        public static Product ToEntity(this ProductDto productDto)
        {
            return new Product
            {
                Id = productDto.Id,
                Name = productDto.Name,
                Description = productDto.Description,
                Price = productDto.Price,
                PictureUrl = productDto.PictureUrl,
                QuantityInStock = productDto.QuantityInStock,
                CategoryId = productDto.CategoryId,
                BrandId = productDto.BrandId,
            };
        }

        public static Product ToEntity(this CreateProductDto createProductDto)
        {
            return new Product
            {
                Name = createProductDto.Name,
                Description = createProductDto.Description,
                Price = createProductDto.Price,
                PictureUrl = createProductDto.PictureUrl,
                QuantityInStock = createProductDto.QuantityInStock,
                CategoryId = createProductDto.CategoryId,
                BrandId = createProductDto.BrandId,
            };
        }
    }
}
