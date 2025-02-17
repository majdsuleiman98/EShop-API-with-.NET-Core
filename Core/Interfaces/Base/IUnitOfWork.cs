using Core.Entities;
using Core.Entities.OrderAggregate;

namespace Core.Interfaces.Base
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<Product> Products { get; }
        IGenericRepository<Brand> Brands { get; }
        IGenericRepository<Category> Categories { get; }
        IGenericRepository<DeliveryMethod> DeliveryMethods { get; }
        IGenericRepository<Order> Orders { get; }
        IGenericRepository<OrderItem> OrderItems { get; }
        Task<int> SaveChangesAsync();
    }
}
