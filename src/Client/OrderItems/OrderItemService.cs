using Shared.OrderItem;
using System.Net.Http.Json;

namespace Client.OrderItems;

public class OrderItemService : IOrderItemService
{
    private readonly HttpClient client;
    private const string endpoint = "api/orderItem";

    public OrderItemService(HttpClient client)
    {
        this.client = client;
    }

    public async Task<OrderItemResponse.GetIndex> GetIndexAsync(OrderItemRequest.GetIndex request)
    {
        var response = await client.GetFromJsonAsync<OrderItemResponse.GetIndex>($"{endpoint}?orderid={request.OrderId}");
        return response;
    }
}
