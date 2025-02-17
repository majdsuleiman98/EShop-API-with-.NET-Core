

using Core.DTOs;
using Core.Entities;
using System.Net;

namespace Core.Mappers
{
    public static class BrandMappingProfile
    {
        public static BrandDto ToDto(this Brand brand)
        {
            return new BrandDto
            {
                Id = brand.Id,
                Name = brand.Name,
            };
        }

        public static Brand ToEntity(this BrandDto brandDto)
        {
            return new Brand
            {
                Id = brandDto.Id,
                Name = brandDto.Name,
            };
        }

        public static Brand ToEntity(this CreateBrandDto createBrandDto)
        {
            return new Brand
            {
                Name = createBrandDto.Name,
            };
        }
    }
}
