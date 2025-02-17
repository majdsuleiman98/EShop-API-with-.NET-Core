
using Core.DTOs;
using Core.Entities;
using Core.Interfaces.Base;
using Core.Interfaces.IServices;
using Core.Mappers;
using Core.Specifications;
using Core.Specifications.SpecParams;
namespace Infrastructure.Services
{
    public class BrandService(IUnitOfWork _unitOfWork) :IBrandService
    {
        public async Task<BrandDto> CreateAsync(CreateBrandDto createBrandDto)
        {
            var brand = createBrandDto.ToEntity();
            _unitOfWork.Brands.Add(brand);
            await _unitOfWork.SaveChangesAsync();
            return brand.ToDto();
        }

        public async Task DeleteAsync(int id)
        {
            var brand = await _unitOfWork.Brands.GetByIdAsync(id);
            if (brand == null) throw new KeyNotFoundException($"Brand with ID {id} not found.");
            _unitOfWork.Brands.Remove(brand);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<PaginatedResponse<BrandDto>> GetAllAsync(BrandSpecParams specParams)
        {
            var spec = new BrandSpecification(specParams);
            var count = await _unitOfWork.Brands.CountAsync(spec);
            var brands =  await _unitOfWork.Brands.GetAllAsync(spec);
            var brandsDto = brands.Select(x => x.ToDto());
            return new PaginatedResponse<BrandDto>(specParams.PageIndex, specParams.PageSize, count, brandsDto);
        }

        public async Task<BrandDto?> GetByIdAsync(int id)
        {
            var brand = await _unitOfWork.Brands.GetByIdAsync(id);
            return brand?.ToDto();
        }

        public async Task UpdateAsync(int id, BrandDto brandDto)
        {
            if(brandDto.Id != id || !_unitOfWork.Brands.IsExists(id)) throw new KeyNotFoundException($"Brand with ID {id} not found or Id mismatch.");
            _unitOfWork.Brands.Update(brandDto.ToEntity());
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
