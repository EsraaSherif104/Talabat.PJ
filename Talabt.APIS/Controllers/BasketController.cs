using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Core.Entities;
using Talabat.Core.Repositories;
using Talabt.APIS.Errors;

namespace Talabt.APIS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : APIBaseController
    {
        private readonly IBasketRepository _basketRepository;

        public BasketController(IBasketRepository basketRepository)
        {
            this._basketRepository = basketRepository;
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
        public async Task<ActionResult<CustomerBasket>>UpdateCustomerbasket(CustomerBasket basket)
        {
          var createdOrUpdatedBasket=    await _basketRepository.UpdateBasketAsync(basket);
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
