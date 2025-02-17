

using Core.DTOs;
using Core.Entities.OrderAggregate;

namespace Core.Mappers
{
    public static class OrderMappingProfile
    {
        public static OrderDto ToDto(this Order order)
        {
            return new OrderDto
            {
                Id = order.Id,
                OrderDate = order.OrderDate,
                BuyerEmail = order.BuyerEmail,
                ShippingAddress = order.ShippingAddress,
                PaymentSummary = order.PaymentSummary,
                DeliveryMethod = order.DeliveryMethod.Description,
                ShippingPrice = order.DeliveryMethod.Price,
                OrderItems = order.OrderItems.Select(x => x.ToDto()).ToList(),
                Subtotal = order.Subtotal,
                Status = order.Status.ToString(),
                PaymentIntentId = order.PaymentIntentId,             
            };
        }

        public static OrderItemDto ToDto(this OrderItem orderItem)
        {
            return new OrderItemDto
            {
                ProductId = orderItem.ItemOrdered.ProductId,
                ProductName = orderItem.ItemOrdered.ProductName,
                PictureUrl = orderItem.ItemOrdered.PictureUrl,
                Price = orderItem.Price,
                Quantity = orderItem.Quantity,
            };
        }
    }
}
