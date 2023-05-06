using Client.Components.Products;
using Client.Products;
using Domain;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Shared.Notifications;
using Shared.Orders;
using Shared.Products;
using System.Reflection.Metadata;
using System.Security.Claims;
using static OneOf.Types.TrueFalseOrNull;

namespace Client.Layout;
public partial class Header
{
    [Inject] public IOrderService? OrderServiceHead { get; set; }
    [Inject] public ShoppingCartState Cart { get; set; } = default!;
    [Inject] public NavigationManager NavigationManager { get; set; } = default!;
    [Inject] public IProductService? ProductService { get; set; }
    private IEnumerable<ProductDto.Index>? _products;
    private string? OrderNrHead { get; set; }
    private IEnumerable<OrderDto.Index>? ordersHead;
    [Parameter] public string NotificationId { get; set; }
    private NotificationDto.Mutate model = new();
    [Inject] public INotificationService? NotificationService { get; set; }
    private IEnumerable<NotificationDto.Index>? notifications;

    protected override async Task OnInitializedAsync()
    {
        // shopping cart
        Cart.OnChange += () => StateHasChanged();
        Cart.OnAdded += () => {
            StateHasChanged();
            // if cart is collapsed, click on it when product is added
            if(collapseShoppingCart == true)
            {
                pressedShoppingCart();
            }
        };

        // notifications
        NotificationRequest.GetIndex request = new();
        var response = await NotificationService.GetIndexAsync(request);
        notifications = response.Notifications;

        //products
        await GetProducts();

        foreach (var p in _products)
        {
            OrderItem item = await localStore.GetItemAsync<OrderItem>(p.ProductId);
            if (item == null)
            {
                Console.WriteLine(p.ProductId.ToString() + ":item is null");
            }
            else
            {
                Cart.AddItem(item);
            }
        }
    }

    private async void MinusOne(OrderItem item)
    {
        Cart.MinusOne(item);
        foreach (var v in Cart.Items)
        {
            if (item.Product.ProductId.Equals(v.Product.ProductId))
            {
                await localStore.SetItemAsync(v.Product.ProductId, v);
            }
        }
    }

    private async void PlusOne(OrderItem item)
    {
        Cart.PlusOne(item);
        foreach (var v in Cart.Items)
        {
            if (item.Product.ProductId.Equals(v.Product.ProductId))
            {
                await localStore.SetItemAsync(v.Product.ProductId, v);
            }
        }
    }

    private async void RemoveItem(OrderItem item)
    {
        foreach (var v in Cart.Items)
        {
            if (item.Product.ProductId.Equals(v.Product.ProductId))
            {
                await localStore.RemoveItemAsync(v.Product.ProductId);
            }
        }
        Cart.RemoveItem(item);
    }

    private async void ClearItems()
    {
        foreach (var v in Cart.Items)
        {
            await localStore.RemoveItemAsync(v.Product.ProductId);
        }
        Cart.ClearItems();
    }

    private void OrderItems()
    {
        collapseShoppingCart = true;
        NavigationManager.NavigateTo("/packageShipment");
    }

    private async Task GetProducts()
    {
        ProductRequest.GetIndex request = new();
        var response = await ProductService.GetIndexAsync(request);
        _products = response.Products;
    }
    private void setModel(NotificationDto.Index index)
    {
        NotificationId = index.NotificationId;
        model.NotificationId = index.NotificationId;
        model.Message = index.Message;
        model.OrderId = index.Order.OrderId;
        model.NotificationDate = index.NotificationDate;
        model.Duration = index.Duration;
        model.IsSeen = 1;
    }

    private async Task EditNotification(NotificationDto.Index index)
    {
        setModel(index);
        NotificationRequest.Edit request = new()
        {
            NotificationId = NotificationId,
            Notification = model
        };
        collapseNotifications = true;
        await NotificationService.EditAsync(request);

        NotificationRequest.GetIndex request2 = new();
        var response = await NotificationService.GetIndexAsync(request2);
        notifications = response.Notifications;
    }
}