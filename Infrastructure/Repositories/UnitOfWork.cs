using Core.Entities;
using Core.Entities.OrderAggregate;
using Core.Interfaces.Base;
using Infrastructure.Data;


namespace Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreContext _context;
        public UnitOfWork(StoreContext context)
        {
            _context = context;
            Products = new GenericRepository<Product>(_context);
            Brands = new GenericRepository<Brand>(_context);
            Categories = new GenericRepository<Category>(_context);
            DeliveryMethods = new GenericRepository<DeliveryMethod>(_context);
            Orders = new GenericRepository<Order>(_context);
            OrderItems = new GenericRepository<OrderItem>(_context);
        }
        public IGenericRepository<Product> Products { get; private set; }
        public IGenericRepository<Brand> Brands { get; private set; }
        public IGenericRepository<Category> Categories { get; private set; }
        public IGenericRepository<DeliveryMethod> DeliveryMethods { get; private set; }
        public IGenericRepository<Order> Orders { get; private set; }
        public IGenericRepository<OrderItem> OrderItems { get; private set; }

        public Task<int> SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
