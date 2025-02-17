

using Core.DTOs;
using Core.Entities;
using System.Runtime.CompilerServices;

namespace Core.Mappers
{
    public static class CategoryMappingProfile
    {
        public static CategoryDto ToDto(this Category category)
        {
            return new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
            };
        }

        public static Category ToEntity(this CategoryDto categoryDto)
        {
            return new Category
            {
                Id = categoryDto.Id,
                Name = categoryDto.Name,
            };
        }

        public static Category ToEntity(this CreateCategoryDto createCategoryDto)
        {
            return new Category
            {
                Name = createCategoryDto.Name,
            };
        }
    }
}
