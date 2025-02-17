

using Core.Entities.OrderAggregate;
using Core.Specifications.SpecParams;

namespace Core.Specifications
{
    public class OrderSpecification : BaseSpecification<Order>
    {
        public OrderSpecification(string email) : 
            base(x => x.BuyerEmail == email) 
        {
            AddInclude(x => x.OrderItems);
            AddInclude(x => x.DeliveryMethod);
            AddOrderByDesc(x => x.OrderDate);
        }

        public OrderSpecification(string email, int orderId) : 
            base(x => (x.BuyerEmail == email) && (x.Id == orderId))
        {
            AddInclude(x => x.OrderItems);
            AddInclude(x => x.DeliveryMethod);
        }

        public OrderSpecification(int orderId) :
            base(x => x.Id == orderId)
        {
            AddInclude(x => x.OrderItems);
            AddInclude(x => x.DeliveryMethod);
        }

        public OrderSpecification(OrderSpecParams specParams) :
            base(x => string.IsNullOrEmpty(specParams.Status) || x.Status == ParseStatus(specParams.Status))
        {
            AddInclude(x => x.OrderItems);
            AddInclude(x => x.DeliveryMethod);
            ApplyPaging(specParams.PageSize * (specParams.PageIndex - 1), specParams.PageSize);
            AddOrderByDesc(x => x.OrderDate);
        }

        public static OrderStatus? ParseStatus(string status)
        {
            if (Enum.TryParse<OrderStatus>(status, true, out var result)) return result;
            return null;
        }
    }
}
