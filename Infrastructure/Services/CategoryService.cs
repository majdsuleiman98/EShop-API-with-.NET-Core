

using Core.DTOs;
using Core.Entities;
using Core.Interfaces.Base;
using Core.Interfaces.IServices;
using Core.Mappers;
using Core.Specifications;
using Core.Specifications.SpecParams;

namespace Infrastructure.Services
{
    public class CategoryService(IUnitOfWork _unitOfWork) : ICategoryService
    {
        public async Task<CategoryDto> CreateAsync(CreateCategoryDto createCategoryDto)
        {
            var category = createCategoryDto.ToEntity();
            _unitOfWork.Categories.Add(category);
            await _unitOfWork.SaveChangesAsync();
            return category.ToDto();
        }

        public async Task DeleteAsync(int id)
        {
            var category = await _unitOfWork.Categories.GetByIdAsync(id);
            if (category == null) throw new KeyNotFoundException($"Category with ID {id} not found.");
            _unitOfWork.Categories.Remove(category);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<PaginatedResponse<CategoryDto>> GetAllAsync(CategorySpecParams specParams)
        {
            var spec = new CategorySpecification(specParams);
            var count = await _unitOfWork.Categories.CountAsync(spec);
            var categories = await _unitOfWork.Categories.GetAllAsync(spec);
            var categoriesDto = categories.Select(x => x.ToDto()).ToList();
            return new PaginatedResponse<CategoryDto>(specParams.PageIndex, specParams.PageSize, count, categoriesDto);
        }

        public async Task<CategoryDto?> GetByIdAsync(int id)
        {
            var category = await _unitOfWork.Categories.GetByIdAsync(id);
            return category?.ToDto();
        }

        public async Task UpdateAsync(int id, CategoryDto categoryDto)
        {
            if (categoryDto.Id != id || !_unitOfWork.Categories.IsExists(id)) throw new KeyNotFoundException($"Brand with ID {id} not found or Id mismatch.");
            _unitOfWork.Categories.Update(categoryDto.ToEntity());
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
