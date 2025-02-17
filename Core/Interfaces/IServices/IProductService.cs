using Core.DTOs;
using Core.Specifications;
using Core.Specifications.SpecParams;

namespace Core.Interfaces.IServices
{
    public interface IProductService
    {
        Task<PaginatedResponse<ProductDto>> GetAllAsync(ProductSpecParams specParams);
        Task<ProductDto?> GetByIdAsync(int id);
        Task<ProductDto> CreateAsync(CreateProductDto createProductDto);
        Task UpdateAsync(int id, ProductDto productDto);
        Task DeleteAsync(int id);
    }
}
