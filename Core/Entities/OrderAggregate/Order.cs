﻿

namespace Core.Entities.OrderAggregate
{
    public class Order : BaseEntity
    {
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;
        public required string BuyerEmail { get; set; }
        public ShippingAddress ShippingAddress { get; set; } = null!;
        public DeliveryMethod DeliveryMethod { get; set; } = null!;
        public PaymentSummary PaymentSummary { get; set; } = null!;
        public ICollection<OrderItem> OrderItems { get; set; } = [];
        public decimal Subtotal { get; set; }
        public required string PaymentIntentId { get; set; }
        public OrderStatus Status { get; set; } = OrderStatus.Pending;
    }
}
