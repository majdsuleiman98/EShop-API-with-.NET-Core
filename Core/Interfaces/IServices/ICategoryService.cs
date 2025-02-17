

using Core.DTOs;
using Core.Entities;
using Core.Specifications;
using Core.Specifications.SpecParams;

namespace Core.Interfaces.IServices
{
    public interface ICategoryService
    {
        Task<PaginatedResponse<CategoryDto>> GetAllAsync(CategorySpecParams specParams);
        Task<CategoryDto?> GetByIdAsync(int id);
        Task<CategoryDto> CreateAsync(CreateCategoryDto createCategoryDto);
        Task UpdateAsync(int id, CategoryDto categoryDto);
        Task DeleteAsync(int id);
    }
}
