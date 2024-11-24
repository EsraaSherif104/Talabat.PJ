using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Core.Services;
using Talabt.APIS.DTO;
using Talabt.APIS.Errors;

namespace Talabt.APIS.Controllers
{
   
    public class PaymentController : APIBaseController
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            this._paymentService = paymentService;
        }
        [ProducesResponseType (typeof(CustomerBasketDTO),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse),StatusCodes.Status400BadRequest)]
        [HttpPost("{basketId}")]
        public async Task<ActionResult<CustomerBasketDTO>> createOrUpdataPaymentIntent(string basketId)
        {
            var basket=await _paymentService.CreateOrUpdatePaymentIntent(basketId);

            if (basket is null) return BadRequest(new ApiResponse(400, "there is a problem with your basket"));
            return Ok (basket);
        }

    }
}
