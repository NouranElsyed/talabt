using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using talabat.Core.ServicesContext;
using talabt.Controllers;
using talabt.Error;
using talabtAPIs.DTOs;

namespace talabtAPIs.Controllers
{
    
    public class PaymentController : BaseApiController
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }
        [HttpPost]
        public async Task<ActionResult<CustomerBasketDTO>> CreateOrUpdatePaymentIntent(string basketId) 
        {
        var basket = await _paymentService.CreateOrUpdatePaymentIntent(basketId);
        if (basket is null) return BadRequest(new ApiErrorResponse( 400,"There is a problem with your basket"));
        return Ok(basket);
        
        }
    }
}
