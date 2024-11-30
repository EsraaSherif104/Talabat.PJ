using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Core.Entities;
using Talabat.Core.Repositories;
using Talabt.APIS.DTO;
using Talabt.APIS.Errors;

namespace Talabt.APIS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : APIBaseController
    {
        private readonly IBasketRepository _basketRepository;
        private readonly Mapper _mapper;

        public BasketController(IBasketRepository basketRepository
            ,Mapper mapper)
        {
            this._basketRepository = basketRepository;
            this._mapper = mapper;
        }
        //get or recreate
        [HttpGet]
        public async Task<ActionResult<CustomerBasket>>GetCustomerBasket(string BasketId)
        {

            
            var basket=await _basketRepository.GetBasketAsync(BasketId);
            //   if (basket == null) return new CustomerBasket(BasketId);
            return basket is null ? new CustomerBasket(BasketId) : basket;

        }

        //update or create
        [HttpPost]
        public async Task<ActionResult<CustomerBasket>>UpdateCustomerbasket(CustomerBasketDTO basket)
        {
            var MappedBasket = _mapper.Map<CustomerBasketDTO, CustomerBasket>(basket);
          var createdOrUpdatedBasket=    await _basketRepository.UpdateBasketAsync(MappedBasket);
            if (createdOrUpdatedBasket == null) return BadRequest(new ApiResponse(400));
            return Ok(createdOrUpdatedBasket);
        }

        //delete
        [HttpDelete]
        public async Task<ActionResult<bool>>DeleteBasket(string basketId)
        {
          return  await _basketRepository.DeleteBasketAsync(basketId);
            
        }
    }
}
