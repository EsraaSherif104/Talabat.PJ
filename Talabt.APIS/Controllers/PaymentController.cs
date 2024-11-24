using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Stripe;
using Stripe.V2;
using System.Linq.Expressions;
using Talabat.Core.Services;
using Talabt.APIS.DTO;
using Talabt.APIS.Errors;

namespace Talabt.APIS.Controllers
{

    public class PaymentController : APIBaseController
    {
        private readonly IPaymentService _paymentService;
        //const string endpointSecrete=

        public PaymentController(IPaymentService paymentService)
        {
            this._paymentService = paymentService;
        }
        [ProducesResponseType(typeof(CustomerBasketDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [HttpPost("{basketId}")]
        public async Task<ActionResult<CustomerBasketDTO>> createOrUpdataPaymentIntent(string basketId)
        {
            var basket = await _paymentService.CreateOrUpdatePaymentIntent(basketId);

            if (basket is null) return BadRequest(new ApiResponse(400, "there is a problem with your basket"));
            return Ok(basket);
        }

        //[HttpPost("webHook")]
        //public async Task<IActionResult> StripewebHook()
        //{
        //    var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
        //    try
        //    {
        //        var stripeEvent = EventUtility.ConstructEvent(json,
        //      Request.Headers["Stripe-Signature"], endpointSecrete);

        //       var paymentIntent= stripeEvent.Data.Object as PaymentIntent;

        //       if (stripeEvent.Type == Events.paymentIntentPaymentFailed)
        //        {
        //            await _paymentService.UpdatePaymentIntentToSuccedOrFailed(paymentIntent.Id,false);
        //        }
        //       else if (stripeEvent.Type == Events.PaymentIntentSucceeded)
        //        {
        //            await _paymentService.UpdatePaymentIntentToSuccedOrFailed(paymentIntent.Id, true);

        //        }
               

        //        return Ok();

        //        }
        //    catch(StripeException e)
        //    {
        //        return BadRequest();
        //    }

        //    }



    }
}
    
