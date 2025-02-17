using Core.DTOs;
using Core.Specifications.SpecParams;
using Core.Specifications;
namespace Core.Interfaces.IServices
{
    public interface IBrandService
    {
        Task<PaginatedResponse<BrandDto>> GetAllAsync(BrandSpecParams specParams);
        Task<BrandDto?> GetByIdAsync(int id);
        Task<BrandDto> CreateAsync(CreateBrandDto createBrandDto);
        Task UpdateAsync(int id, BrandDto brandDto);
        Task DeleteAsync(int id);
    }
}
