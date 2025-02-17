

namespace Core.Entities.OrderAggregate
{
    public class ShippingAddress
    {
        public required string Name { get; set; }
        public required string Line { get; set; }
        public required string City { get; set; }
        public required string State { get; set; }
        public required string Zipcode { get; set; }
        public required string Country { get; set; }
    }
}
