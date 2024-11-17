using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using talabat.Core.ServicesContext;
using talabt.Controllers;
using talabt.Error;
using talabtAPIs.DTOs;

namespace talabtAPIs.Controllers
{

    public class PaymentController : BaseApiController
    {
        private readonly IPaymentService _paymentService;
        const string endpointSecret = "whsec_f8d20dfd2aedbc539472fbb905c98cdb21f4148654e21148b41f75df196a1c78";

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }
        [HttpPost("{basketId}")]
        public async Task<ActionResult<CustomerBasketDTO>> CreateOrUpdatePaymentIntent(string basketId)
        {
            var basket = await _paymentService.CreateOrUpdatePaymentIntent(basketId);
            Console.WriteLine($"PaymentIntentId: {basket.PaymentIntentId}");
            Console.WriteLine($"ClientSecret: {basket.ClientSecret}");
            if (basket is null) return BadRequest(new ApiErrorResponse(400, "There is a problem with your basket"));
            return Ok(basket);

        }

        [HttpPost("webhook")]
        public async Task<IActionResult> StripeWebHooks() 
        {
         
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
            try
            {
                

                var stripeEvent = EventUtility.ConstructEvent(json,
                        Request.Headers["Stripe-Signature"], endpointSecret);
                var paymentIntent = stripeEvent.Data.Object as PaymentIntent;
                var stripeEventType = stripeEvent?.Type; 
                var paymentIntentId = paymentIntent?.Id;

                // If on SDK version < 46, use class Events instead of EventTypes
                if (stripeEvent.Type == EventTypes.PaymentIntentSucceeded)
                {
                    await _paymentService.ChangeOrderStatus(paymentIntentId, true);

                    Console.WriteLine("A successful payment for {0} was made.", paymentIntent.Amount);
                    // Then define and call a method to handle the successful payment intent.
                    // handlePaymentIntentSucceeded(paymentIntent);
                }
                else if (stripeEvent.Type == EventTypes.PaymentIntentPaymentFailed)
                {
                   await _paymentService.ChangeOrderStatus(paymentIntentId, false);

                    // Then define and call a method to handle the successful attachment of a PaymentMethod.
                    // handlePaymentMethodAttached(paymentMethod);
                }
                
                return Ok();
            }
            catch (StripeException e)
            {
                Console.WriteLine("Error: {0}", e.Message);
                return BadRequest();
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }
    
    }
}
