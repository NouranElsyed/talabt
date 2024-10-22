using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using talabat.Core.Entities;
using talabat.Core.RepositoriesContext;
using talabt.Controllers;
using talabt.Error;
using talabtAPIs.DTOs;

namespace talabtApi.Controllers
{
    public class BasketController : BaseApiController
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IMapper _mapper;

        public BasketController(IBasketRepository basketRepository,IMapper mapper)
        {
            _basketRepository = basketRepository;
            _mapper = mapper;
        }

        [HttpGet("{basketid}")]
        public async Task<ActionResult<CustomerBasket>> GetCustomerBasket(string basketid) 
        {
            var basket = await _basketRepository.GetBasketAsync(basketid); 
            return basket is null ? new CustomerBasket (basketid) : Ok(basket);
        }
        [HttpPost]
        public async Task<ActionResult<CustomerBasket>> UpdateCustomerBasket(CustomerBasketDTO basket) 
        {
            var MappedBasket = _mapper.Map<CustomerBasketDTO, CustomerBasket>(basket);
            var CreatedOrUpdatedBasked = await _basketRepository.UpdateBasketAsync(MappedBasket);
            if (CreatedOrUpdatedBasked is null) return BadRequest(new ApiErrorResponse(400));
            return Ok(CreatedOrUpdatedBasked);
        }

        [HttpDelete]

        public async Task<ActionResult<bool>> DeleteCustomerBasket(string basketid )
        {
          
            return await _basketRepository.DeleteBasketAsync(basketid);

        }

    }
}
