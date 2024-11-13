using Microsoft.Extensions.Configuration;
using Stripe;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using talabat.Core;
using talabat.Core.Entities;
using talabat.Core.Entities.Order_Aggregate;
using talabat.Core.RepositoriesContext;
using talabat.Core.ServicesContext;

namespace talabat.Services.Services.Payment
{
    public class PaymentService : IPaymentService
    {
        private readonly IBasketRepository _basketRepo;
        private readonly IConfiguration _configuration;
        private readonly IUnitOfWork _unitOfWork;

        public PaymentService(IBasketRepository basketRepo,IConfiguration configuration,IUnitOfWork unitOfWork)
        {
            _basketRepo = basketRepo;
            _configuration = configuration;
            _unitOfWork = unitOfWork;
        }
        public async Task<CustomerBasket> CreateOrUpdatePaymentIntent(string BasketId)
        {
            StripeConfiguration.ApiKey = _configuration["StripeSettings:Secretkey"];
            var Basket = await _basketRepo.GetBasketAsync(BasketId);
            if (Basket is null) return null;
            //Amount = subtotal + DeliveryMethod
            var ShippingCost = 0M;

            if (Basket.DeliveryMethodId.HasValue)
            {
                var DeliveryMethod = await _unitOfWork.Repository<DeliveryMethod>().GetAsync(Basket.DeliveryMethodId.Value);
                ShippingCost = DeliveryMethod.Cost;
            }

            if (Basket.items.Count > 0) 
            {
                foreach (var item in Basket.items) 
                {
                    var product = await _unitOfWork.Repository<Products>().GetAsync(item.Id);
                    if (item.Price != product.Price) 
                    {
                        item.Price = product.Price;
                    }
                
                }
            }
            var subtotal = Basket.items.Sum(item => item.Quantity*item.Price);
            var service = new PaymentIntentService();
            PaymentIntent paymentIntent;


            if (Basket.PaymentIntentId is null)
            {
                var options = new PaymentIntentCreateOptions()
                {
                    Amount = (long)ShippingCost * 100 + (long)subtotal * 100,
                    Currency="usd",
                    PaymentMethodTypes= new List<string>() {"card"}
                };
                paymentIntent = await service.CreateAsync(options);
                Basket.PaymentIntentId = paymentIntent.Id;
                Basket.ClientSecret = paymentIntent.ClientSecret;

            }
            else 
            {
                var options = new PaymentIntentUpdateOptions()
                {
                    Amount = (long)ShippingCost * 100 + (long)subtotal * 100
                };
                paymentIntent = await service.UpdateAsync(Basket.PaymentIntentId,options);
                Basket.PaymentIntentId = paymentIntent.Id;
                Basket.ClientSecret = paymentIntent.ClientSecret;
            }
            await _basketRepo.UpdateBasketAsync(Basket);
            return Basket;
             
        }
    }
}
