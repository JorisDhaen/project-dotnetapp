using Microsoft.AspNetCore.Mvc;

using Shared.Orders;
using Swashbuckle.AspNetCore.Annotations;

namespace Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService orderService;

        public OrderController(IOrderService orderService)
        {
            this.orderService = orderService;
        }

        [SwaggerOperation("Returns a list of all orders.")]
        [HttpGet]
        public Task<OrderResponse.GetIndex> GetIndex([FromQuery] OrderRequest.GetIndex request)
        {
            return orderService.GetIndexAsync(request);
        }

        [SwaggerOperation("Returns a list of orders from user.")]
        [HttpGet("user/{UserId}")]
        public Task<OrderResponse.GetIndex> GetIndexUSer([FromRoute] OrderRequest.GetIndexUser request)
        {
            return orderService.GetIndexUserAsync(request);
        }

        [SwaggerOperation("Returns a order from a user")]
        [HttpGet("{OrderId}")]
        public Task<OrderResponse.GetDetail> GetDetail([FromRoute] OrderRequest.GetDetail request)
        {
            return orderService.GetDetailAsync(request);
        }


        [SwaggerOperation("Makes a new order")]
        [HttpPost]
        public Task<OrderResponse.Create> CreateAsync([FromBody] OrderRequest.Create request)
        {
            return orderService.CreateAsync(request);
        }
        [SwaggerOperation("Returns a list of notifications from order.")]
        [HttpGet("notifications/{OrderId}")]
        public Task<OrderResponse.OrderNotifications> GetOrderNotifications([FromRoute] OrderRequest.GetDetail request)
        {
            Console.WriteLine(request.OrderId);
            return orderService.GetNotificationsOrderAsync(request);
        }

        [SwaggerOperation("Returns a list of orderitems from order.")]
        [HttpGet("orderitems/{OrderId}")]
        public Task<OrderResponse.OrderItems> GetOrderItems([FromRoute] OrderRequest.GetDetail request)
        {
            Console.WriteLine(request.OrderId);
            return orderService.GetOrderItemsOrderAsync(request); 
        }

        [SwaggerOperation("Edites an existing order his orderstatus.")]
        [HttpPut("{orderId}")]
        public async Task<IActionResult> Edit(string orderId, OrderDto.Mutate model)
        {
            await orderService.EditAsync(orderId, model);
            return NoContent();
        }

    }
}
