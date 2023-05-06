using Azure.Core;
using Shared.Orders;
using System.Net.Http.Json;

namespace Client.Orders;

public class OrderService : IOrderService
{
    private readonly HttpClient client;
    private const string endpoint = "api/order";

    public OrderService(HttpClient client)
    {
        this.client = client;
    }

    public async Task<OrderResponse.GetIndex> GetIndexAsync(OrderRequest.GetIndex request)
    {
        var response = await client.GetFromJsonAsync<OrderResponse.GetIndex>($"{endpoint}");
        return response;
    }
    public async Task<OrderResponse.GetIndex> GetIndexUserAsync(OrderRequest.GetIndexUser request)
    {
        Console.WriteLine("The ID");
        Console.WriteLine(request.UserId);
        var response = await client.GetFromJsonAsync<OrderResponse.GetIndex>($"{endpoint}/user/{request.UserId}");
        return response;
    }

    public async Task<OrderResponse.GetDetail> GetDetailAsync(OrderRequest.GetDetail request)
    {
        var response = await client.GetFromJsonAsync<OrderResponse.GetDetail>($"{endpoint}/{request.OrderId}");
        return response;
    }

    public async Task<OrderResponse.Create> CreateAsync(OrderRequest.Create request)
    {
        var reponse = await client.PostAsJsonAsync(endpoint, request);
        return await reponse.Content.ReadFromJsonAsync<OrderResponse.Create>();
    }
    public async Task<OrderResponse.OrderNotifications> GetNotificationsOrderAsync(OrderRequest.GetDetail request)
    {
        var response = await client.GetFromJsonAsync<OrderResponse.OrderNotifications>($"{endpoint}/notifications/{request.OrderId}");
        return response;
    }

    public async Task<OrderResponse.OrderItems> GetOrderItemsOrderAsync(OrderRequest.GetDetail request)
    {
        var response = await client.GetFromJsonAsync<OrderResponse.OrderItems>($"{endpoint}/orderitems/{request.OrderId}");
        return response;
    }

    public Task EditAsync(string OrderId, OrderDto.Mutate model)
    {
        throw new NotImplementedException();
    }

  
}
