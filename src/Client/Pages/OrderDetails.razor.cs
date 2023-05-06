using Microsoft.AspNetCore.Components;
using Shared.Orders;
using Shared.Notifications;

namespace Client.Pages;

public partial class OrderDetails
{
    private OrderDto.Index? order;
    private IEnumerable<NotificationDto.Index>? notifications;
    [Inject] public IOrderService OrderService { get; set; } = default!;
    [Parameter] public string Id { get; set; }
    public DateTime? ArrivalTime { get; set; } = null!;
    public DateTime? ArrivalTime2 { get; set; } = null!;


    protected async override Task OnInitializedAsync()
    {
        await GetOrder();
        //order = await OrderService.GetOrderDetailAsync(Id);
        await GetNotificationsOrder();
    }

    private async Task GetOrder()
    {
        OrderRequest.GetDetail request = new() { OrderId = Id };
        var response = await OrderService.GetDetailAsync(request);
        order = response.Order;
        ArrivalTime = berekenArrivalTime();
    }

    private async Task GetNotificationsOrder()
    {
        OrderRequest.GetDetail request = new() { OrderId = Id };
        var response = await OrderService.GetNotificationsOrderAsync(request);
        notifications = response.NotificationsOrder;
    }

    private DateTime berekenArrivalTime()
    {
        DateTime dateTime;
        DateTime statusChanged = order!.StatusDate;
        switch (order.OrderStatus)
        {
            case 1:
                dateTime = statusChanged.AddDays(3);
                break;
            case 2:
                dateTime = statusChanged.AddDays(1);
                break;
            case 3:
                dateTime = statusChanged.AddHours(4);
                break;
            case 4:
                dateTime = DateTime.Now;
                break;
            default:
                dateTime = new DateTime(2010, 5, 1, 8, 30, 52);
                break;
        }
        return dateTime;
    }

    private void estamateArrivalTime(DateTime arrival)
    {
        ArrivalTime = arrival;
    }
}
