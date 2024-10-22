using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using talabat.Core.Entities.Order_Aggregate;
using talabat.Core.ServicesContext;
using talabt.Controllers;
using talabt.Error;
using talabtAPIs.DTOs;

namespace talabtAPIs.Controllers
{

    public class OrdersController : BaseApiController
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

        public OrdersController(IOrderService orderService,IMapper mapper)
        {
            
            _orderService = orderService;
            _mapper = mapper;
        }
        [ProducesResponseType(typeof(Order) , StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse),StatusCodes.Status400BadRequest)]
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Order>> CreateOrder(OrderDTO orderDto) 
        {
            var BuyerEmail = User.FindFirstValue(ClaimTypes.Email);
            var MappedAddress = _mapper.Map<AddressDTO,Address>(orderDto.shippingAddress);
            var order = await _orderService.CreateOrderAsync(BuyerEmail,orderDto.basketId,orderDto.deliveryMethodId,MappedAddress);
            if (order is null) return BadRequest(new ApiErrorResponse(400,"There is a Problem with your order"));
            return Ok(order);

        }

    }
}
