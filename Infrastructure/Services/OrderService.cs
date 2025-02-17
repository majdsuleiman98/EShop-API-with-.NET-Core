

using Core.DTOs;
using Core.Entities;
using Core.Entities.OrderAggregate;
using Core.Interfaces.Base;
using Core.Interfaces.IServices;
using Core.Mappers;
using Core.Specifications;

namespace Infrastructure.Services
{
    public class OrderService(ICartService cartService, IUnitOfWork unitOfWork) : IOrderService
    {
        public async Task<OrderDto> CreateOrder(CreateOrderDto createOrderDto, string buyerEmail)
        {
            var cart = await cartService.GetCartAsync(createOrderDto.CartId);
            if (cart == null) throw new ArgumentException("Cart not found");
            if (cart.PaymentIntentId == null) throw new ArgumentException("No payment intent for this cart");
            var items = await GetOrderItems(cart);
            if(items == null) throw new ArgumentException("Problem with product in cart");
            var deliveryMethod = await unitOfWork.DeliveryMethods.GetByIdAsync(createOrderDto.DeliveryMethodId);
            if (deliveryMethod == null) throw new ArgumentException("Problem with Delivery Method");
            var order = CreateNewOrder(items, deliveryMethod, createOrderDto, buyerEmail, cart.PaymentIntentId);
            unitOfWork.Orders.Add(order);
            await unitOfWork.SaveChangesAsync();
            await cartService.DeleteCartAsync(createOrderDto.CartId);
            return order.ToDto();
        }
        private async Task<List<OrderItem>> GetOrderItems(ShoppingCart cart)
        {
            var items = new List<OrderItem>();
            foreach (var item in cart.Items)
            {
                var productItem = await unitOfWork.Products.GetByIdAsync(item.ProductId);
                if (productItem == null) throw new ArgumentException("Problem with product in cart");
                var itemOrderd = new ProductItemOrdered
                {
                    ProductId = item.ProductId,
                    ProductName = item.ProductName,
                    PictureUrl = item.PictureUrl,
                };
                var orderItem = new OrderItem
                {
                    ItemOrdered = itemOrderd,
                    Price = productItem.Price,
                    Quantity = item.Quantity,
                };
                items.Add(orderItem);
            }
            return items;
        } 

        private Order CreateNewOrder(List<OrderItem> items, DeliveryMethod deliveryMethod, CreateOrderDto createOrderDto, string buyerEmail, string PaymentIntentId)
        {
            var order = new Order
            {
                OrderItems = items,
                DeliveryMethod = deliveryMethod,
                ShippingAddress = createOrderDto.ShippingAddress,
                Subtotal = items.Sum(x => x.Price * x.Quantity) + deliveryMethod.Price,
                PaymentSummary = createOrderDto.PaymentSummary,
                PaymentIntentId = PaymentIntentId,
                BuyerEmail = buyerEmail,
            };
            return order;
        }

        public async Task<List<OrderDto>> GetOrdersOfUser(string buyerEmail)
        {
            var spec = new OrderSpecification(buyerEmail);
            var orders = await unitOfWork.Orders.GetAllAsync(spec);
            var ordersDto = orders.Select(x => x.ToDto()).ToList();
            return ordersDto;
        }
        public async Task<OrderDto?> GetOrderOfUser(string buyerEmail, int orderId)
        {
            var spec = new OrderSpecification(buyerEmail, orderId);
            var order = await unitOfWork.Orders.GetByIdAsync(spec);
            return order?.ToDto();
        }
    }
}
