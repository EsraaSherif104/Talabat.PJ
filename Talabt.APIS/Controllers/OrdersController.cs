using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;
using System.Security.Claims;
using Talabat.Core;
using Talabat.Core.Entities.Core_Aggra;
using Talabat.Core.Entities.Order_Aggra;
using Talabat.Core.Services;
using Talabt.APIS.DTO;
using Talabt.APIS.Errors;

namespace Talabt.APIS.Controllers
{

    public class OrdersController : APIBaseController
    {
        private readonly IOrderServices _orderServices;
        private readonly IMapper _mapper;
        private readonly IUniteOfWork _uniteOfWork;

        public OrdersController(IOrderServices orderServices,IMapper mapper
            ,IUniteOfWork uniteOfWork)
        {
            this._orderServices = orderServices;
            this._mapper = mapper;
            this._uniteOfWork = uniteOfWork;
        }

        //CRREATE ORDER
        [ProducesResponseType(typeof(Talabat.Core.Entities.Order_Aggra.Order) , StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse),StatusCodes.Status400BadRequest)]
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Talabat.Core.Entities.Order_Aggra.Order>> CreateOrder(OrderDto orderDto)
        {
            var BuyerEmail = User.FindFirstValue(ClaimTypes.Email);
            var mappedadress = _mapper.Map<AddressDTO, Address>(orderDto.Shippingaddress);
            var Order=  await  _orderServices.CreateOrderAsync(BuyerEmail, orderDto.BasketId, orderDto.DeliveryMethodId, mappedadress);

            if (Order is null) return BadRequest(new ApiResponse(400, "there is a problem with your order"));
            return Ok(Order);
            //  _orderServices.CreateOrderAsync()

        }

        [ProducesResponseType(typeof(ApiResponse),StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(IReadOnlyList<Talabat.Core.Entities.Order_Aggra.Order>) , StatusCodes.Status200OK)]
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<OrderToReturnDTO>>> GetOrdersForUser() 
        {
            var BuyerEmail = User.FindFirstValue(ClaimTypes.Email);
            var orders = await _orderServices.GetOrderWithSpecificUserAsync(BuyerEmail);
            if (orders is null) return NotFound(new ApiResponse(404, "there is no orders for this user"));
            var mappedOrders = _mapper.Map<IReadOnlyList<Talabat.Core.Entities.Order_Aggra.Order>, IReadOnlyList<OrderToReturnDTO>>  (orders);
            
            return Ok(mappedOrders);
        
        }
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Talabat.Core.Entities.Order_Aggra.Order), StatusCodes.Status200OK)]

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<Talabat.Core.Entities.Order_Aggra.Order>>GetOrdersBYIDForUser(int id)
        {
            var BuyerEmail = User.FindFirstValue(ClaimTypes.Email);
            var order = await _orderServices.GetOrderByIdWithSpecificUserAsync(BuyerEmail,id);
            if (order is null) return NotFound(new ApiResponse(404, $"there is no order with {id} for  this user"));
            var mappedOrders = _mapper.Map<Talabat.Core.Entities.Order_Aggra.Order,OrderToReturnDTO> (order);

            return Ok(mappedOrders);
        }

        [HttpGet("DeliveryMethods")]

        public async Task<ActionResult<IReadOnlyList<DeliveryMethod>>> GetDeliveryMethod()
        {
            var DeliveryMethod = await _uniteOfWork.Repository<DeliveryMethod>().GetAllAsync();
            return Ok(DeliveryMethod);
        }


    }
}
