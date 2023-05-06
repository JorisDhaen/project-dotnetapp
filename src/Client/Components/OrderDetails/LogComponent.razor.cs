using Microsoft.AspNetCore.Components;
using Shared.Orders;
using Domain;
using Client.Orders;
using Server.Models;
using Shared.Notifications;
using Shared.OrderItem;
using Shared.Products;

namespace Client.Components.OrderDetails;

public partial class LogComponent
{
    [Parameter, EditorRequired] public IEnumerable<NotificationDto.Index>? Notifications { get; set; }
    [Parameter, EditorRequired] public OrderDto.Index? Order { get; set; }
    [Inject] public IOrderItemService? OrderItemService { get; set; }
    private IEnumerable<OrderItemDto.Index>? orderItem;
    private IEnumerable<ProductDto.Index>? _products;
    private List<ProductDto.Index>? producten;
    [Inject] public IProductService? ProductService { get; set; }

    private string setLogMessage(NotificationDto.Index notification)
    {
        return String.Format(notification?.NotificationDate.ToShortDateString() + " | " + notification?.NotificationDate.ToShortTimeString() + " | " + notification?.Message);
    }

    /*protected override async Task OnInitializedAsync()
    {
        OrderItemRequest.GetIndex request = new()
        {
            OrderId = Order.OrderId
        };
        var response = await OrderItemService.GetIndexAsync(request);
        orderItem = response.OrderItems;

        ProductRequest.GetIndex request2 = new();
        var response2 = await ProductService.GetIndexAsync(request2);
        _products = response2.Products;
        foreach(var item in orderItem)
        {
            foreach (var product in _products)
            {
                @Console.WriteLine(item.OrderId + product.Name);
                if (item.ProductId == product.ProductId)
                {
                    producten.Add(product);
                }
            }
        }
    }*/
}