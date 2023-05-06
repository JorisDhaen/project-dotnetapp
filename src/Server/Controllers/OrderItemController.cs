using Microsoft.AspNetCore.Mvc;
using Shared.OrderItem;
using Swashbuckle.AspNetCore.Annotations;

namespace Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrderItemController : ControllerBase
{
    private readonly IOrderItemService orderItemService;

    public OrderItemController(IOrderItemService orderItemService)
    {
        this.orderItemService = orderItemService;
    }

    [SwaggerOperation("Returns a list of orderItems of an order")]
    [HttpGet]
    public Task<OrderItemResponse.GetIndex> GetIndex([FromQuery] OrderItemRequest.GetIndex request)
    {
        return orderItemService.GetIndexAsync(request);
    }
}
