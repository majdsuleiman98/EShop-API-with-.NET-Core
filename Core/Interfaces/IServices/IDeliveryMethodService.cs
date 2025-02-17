

using Core.DTOs;
using Core.Specifications.SpecParams;
using Core.Specifications;

namespace Core.Interfaces.IServices
{
    public interface IDeliveryMethodService
    {
        Task<PaginatedResponse<DeliveryMethodDto>> GetAllAsync(DeliveryMethodSpecParams specParams);
        Task<DeliveryMethodDto?> GetByIdAsync(int id);
        Task<DeliveryMethodDto> CreateAsync(CreateDeliveryMethodDto createDeliveryMethodDto);
        Task UpdateAsync(int id, DeliveryMethodDto methodDto);
        Task DeleteAsync(int id);
    }
}
