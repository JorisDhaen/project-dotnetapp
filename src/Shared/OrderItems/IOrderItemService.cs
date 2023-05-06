namespace Shared.OrderItem;

public interface IOrderItemService
{
    Task<OrderItemResponse.GetIndex> GetIndexAsync(OrderItemRequest.GetIndex request);
}
