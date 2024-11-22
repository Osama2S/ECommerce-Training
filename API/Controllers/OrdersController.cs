using API.DTO;
using API.Errors;
using API.Extensions;
using AutoMapper;
using Core.Entities.OrderAggregate;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;
        public readonly IMapper _mapper;

        public OrdersController(IOrderService orderService , IMapper mapper)
        {
            _orderService=orderService;
            _mapper=mapper;
        }


        [HttpPost]
        public async Task<ActionResult<Order>> CreateOrder(OrderDto orderDto)
        {
            var email = HttpContext.User.RetriveEmailFromPrincipel();
            var address = _mapper.Map<AddressDTO, Address>(orderDto.ShipToAddress!);
            var order = await _orderService.CreateOrderAsync(email, orderDto.DeliveryMethodId, orderDto.BasketId, address);
             if (order == null) return BadRequest(new APIResponse(400, "Problem creating order"));
             return Ok(order);
            
        }
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<OrderDto>>> GetOrdersForUser()
        {
            var email = HttpContext.User.RetriveEmailFromPrincipel();
            var orders = await _orderService.GetOrdersForUserAsync(email);
            return Ok(_mapper.Map<IReadOnlyList<Order>,IReadOnlyList<OrderToReturnDto>>(orders));
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderToReturnDto>>GetOrderById(int id)
        {
            var email = HttpContext.User.RetriveEmailFromPrincipel();
            var order = await _orderService.GetOrderByIdAsync(id, email);
            if (order==null) return NotFound(new APIResponse(400));
            return _mapper.Map<Order, OrderToReturnDto>(order);
        }
        [HttpGet("deliveryMethods")]
        public async Task<ActionResult<IReadOnlyList<DeliveryMethod>>> GetDeliveryMethod()
        {
            return Ok(await _orderService.GetDeliveryMethodsAsync());
        }
    }
}
