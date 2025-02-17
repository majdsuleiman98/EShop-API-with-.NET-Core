

using Core.Entities;

namespace Core.Interfaces.IServices
{
    public interface IPaymentService
    {
        Task<ShoppingCart?> CreateOrUpdatePaymetnIntent(string cartId);
    }
}
