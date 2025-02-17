
using Core.DTOs;
using Core.Entities.OrderAggregate;

namespace Core.Interfaces.IServices
{
    public interface IOrderService
    {
        public Task<OrderDto> CreateOrder(CreateOrderDto createOrderDto, string buyerEmail);
        public Task<List<OrderDto>> GetOrdersOfUser(string buyerEmail);
        public Task<OrderDto?> GetOrderOfUser(string buyerEmail, int orderId);
    }
}
