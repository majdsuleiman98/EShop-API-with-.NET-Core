

using Core.Entities;
using Core.Interfaces.Base;
using Core.Interfaces.IServices;
using Microsoft.Extensions.Configuration;
using Stripe;

namespace Infrastructure.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICartService _cartService;
        public PaymentService(IConfiguration config, ICartService cartService, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _cartService = cartService;
            StripeConfiguration.ApiKey = config["StripeSettings:SecretKey"];
        }
        public async Task<ShoppingCart?> CreateOrUpdatePaymetnIntent(string cartId)
        {
            var cart = await _cartService.GetCartAsync(cartId) ?? throw new Exception("Cart unavailable");
            var shippingPrice = await GetShippingPriceAsync(cart) ?? 0;
            await ValidateCartItemsInCartAsync(cart);
            var subtotal = CalculateSubtotal(cart);
            var total = subtotal + shippingPrice;
            await CreateUpdatePaymentIntentAsync(cart, total);
            await _cartService.SetCartAsync(cart);
            return cart;
        }

        private async Task<long?> GetShippingPriceAsync(ShoppingCart cart)
        {
            if (cart.DeliveryMethodId.HasValue)
            {
                var deliveryMethod = await _unitOfWork.DeliveryMethods.GetByIdAsync(cart.DeliveryMethodId.Value) ?? throw new Exception("Problem with delivery method");
                return (long)deliveryMethod.Price * 100;
            }
            return null;
        }

        private async Task ValidateCartItemsInCartAsync(ShoppingCart cart)
        {
            foreach (var item in cart.Items)
            {
                var productItem = await _unitOfWork.Products.GetByIdAsync(item.ProductId) ?? throw new Exception("Problem getting product in cart");
                if (item.Price != productItem.Price)
                {
                    item.Price = productItem.Price;
                }
            }
        }

        private long CalculateSubtotal(ShoppingCart cart)
        {
            var subtotal = cart.Items.Sum(x => x.Quantity * x.Price * 100);
            return (long)subtotal;
        }

        private async Task CreateUpdatePaymentIntentAsync(ShoppingCart cart, long total)
        {
            var service = new PaymentIntentService();
            if (string.IsNullOrEmpty(cart.PaymentIntentId))
            {
                var options = new PaymentIntentCreateOptions
                {
                    Amount = total,
                    Currency = "usd",
                    PaymentMethodTypes = ["card"]
                };
                var intent = await service.CreateAsync(options);
                cart.PaymentIntentId = intent.Id;
                cart.ClientSecret = intent.ClientSecret;
            }
            else
            {
                var options = new PaymentIntentUpdateOptions
                {
                    Amount = total,
                };
                await service.UpdateAsync(cart.PaymentIntentId, options);
            }
        }


    }
}
