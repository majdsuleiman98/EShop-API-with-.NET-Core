using Core.DTOs;
using Core.Interfaces.Base;
using Core.Interfaces.IServices;
using Core.Mappers;
using Core.Specifications;
using Core.Specifications.SpecParams;


namespace Infrastructure.Services
{
    public class AdminOrderService(IUnitOfWork unitOfWork) : IAdminOrderService
    {
        public async Task<PaginatedResponse<OrderDto>> GetAllAsync(OrderSpecParams specParams)
        {
            var spec = new OrderSpecification(specParams);
            var count = await unitOfWork.Orders.CountAsync(spec);
            var orders = await unitOfWork.Orders.GetAllAsync(spec);
            var ordersDto = orders.Select(x => x.ToDto());
            return new PaginatedResponse<OrderDto>(specParams.PageIndex, specParams.PageSize, count, ordersDto);
        }        

        public async Task<OrderDto?> GetByIdAsync(int id)
        {
            var spec = new OrderSpecification(id);
            var order = await unitOfWork.Orders.GetByIdAsync(spec);
            return order?.ToDto();
        }
    }
}
