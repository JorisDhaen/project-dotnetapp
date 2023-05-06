
namespace Shared.Orders;
public interface IOrderService
{
    // GEBRUIKT DOOR REAL SERVICE --> HAALT DUS DATA UIT DATABANK
    Task<OrderResponse.GetIndex> GetIndexAsync(OrderRequest.GetIndex request);
    Task<OrderResponse.GetIndex> GetIndexUserAsync(OrderRequest.GetIndexUser request);
    Task<OrderResponse.GetDetail> GetDetailAsync(OrderRequest.GetDetail request);
    Task<OrderResponse.Create> CreateAsync(OrderRequest.Create request);
    Task<OrderResponse.OrderNotifications> GetNotificationsOrderAsync(OrderRequest.GetDetail request);
    Task<OrderResponse.OrderItems> GetOrderItemsOrderAsync(OrderRequest.GetDetail request);
    Task EditAsync(string OrderId, OrderDto.Mutate model);
}
