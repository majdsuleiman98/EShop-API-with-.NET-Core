

using Core.DTOs;
using Core.Entities;
using Core.Interfaces.Base;
using Core.Interfaces.IServices;
using Core.Mappers;
using Core.Specifications;
using Core.Specifications.SpecParams;

namespace Infrastructure.Services
{
    public class DeliveryMethodService(IUnitOfWork _unitOfWork) : IDeliveryMethodService
    {
        public async Task<DeliveryMethodDto> CreateAsync(CreateDeliveryMethodDto createDeliveryMethodDto)
        {
            var method = createDeliveryMethodDto.ToEntity();
            _unitOfWork.DeliveryMethods.Add(method);
            await _unitOfWork.SaveChangesAsync();
            return method.ToDto();  
        }

        public async Task DeleteAsync(int id)
        {
            var method = await _unitOfWork.DeliveryMethods.GetByIdAsync(id);
            if (method == null) throw new KeyNotFoundException($"Delivery Method with ID {id} not found.");
            _unitOfWork.DeliveryMethods.Remove(method);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<PaginatedResponse<DeliveryMethodDto>> GetAllAsync(DeliveryMethodSpecParams specParams)
        {
            var spec = new DeliveryMethodSpecification(specParams);
            var count = await _unitOfWork.DeliveryMethods.CountAsync(spec);
            var methods = await _unitOfWork.DeliveryMethods.GetAllAsync(spec);
            var methodsDto = methods.Select(x => x.ToDto()).ToList();
            return new PaginatedResponse<DeliveryMethodDto>(specParams.PageIndex, specParams.PageSize, count, methodsDto);
        }

        public async Task<DeliveryMethodDto?> GetByIdAsync(int id)
        {
            var method = await _unitOfWork.DeliveryMethods.GetByIdAsync(id);
            return method?.ToDto();
        }

        public async Task UpdateAsync(int id, DeliveryMethodDto methodDto)
        {
            if (methodDto.Id != id || !_unitOfWork.DeliveryMethods.IsExists(id)) throw new KeyNotFoundException($"Delivery method with ID {id} not found or Id mismatch.");
            _unitOfWork.DeliveryMethods.Update(methodDto.ToEntity());
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
