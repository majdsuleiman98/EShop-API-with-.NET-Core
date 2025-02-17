using Core.DTOs;
using Core.Entities;
using Core.Interfaces.Base;
using Core.Interfaces.IServices;
using Core.Mappers;
using Core.Specifications;
using Core.Specifications.SpecParams;


namespace Infrastructure.Services
{
    public class ProductService(IUnitOfWork _unitOfWork) : IProductService
    {
        public async Task<ProductDto> CreateAsync(CreateProductDto createProductDto)
        {
            var product = createProductDto.ToEntity();
            _unitOfWork.Products.Add(product);
            await _unitOfWork.SaveChangesAsync();
            return product.ToDto();
        }

        public async Task DeleteAsync(int id)
        {
            var product = await _unitOfWork.Products.GetByIdAsync(id);
            if (product == null) throw new KeyNotFoundException($"Product with ID {id} not found.");
            _unitOfWork.Products.Remove(product);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<PaginatedResponse<ProductDto>> GetAllAsync(ProductSpecParams specParams)
        {
            var spec = new ProductSpecification(specParams);
            var count = await _unitOfWork.Products.CountAsync(spec);
            var products = await _unitOfWork.Products.GetAllAsync(spec);
            var productsDto =  products.Select(x => x.ToDto()).ToList();    
            return new PaginatedResponse<ProductDto>(specParams.PageIndex, specParams.PageSize, count, productsDto);
        }

        public async Task<ProductDto?> GetByIdAsync(int id)
        {
            var spec = new ProductSpecification(id);
            var product = await _unitOfWork.Products.GetByIdAsync(spec);
            return product?.ToDto();
        }

        public async Task UpdateAsync(int id, ProductDto productDto)
        {
            if (productDto.Id != id || !_unitOfWork.Products.IsExists(id)) throw new KeyNotFoundException($"Brand with ID {id} not found or Id mismatch.");
            _unitOfWork.Products.Update(productDto.ToEntity());
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
