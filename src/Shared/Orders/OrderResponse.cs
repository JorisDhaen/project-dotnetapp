using Shared.Notifications;

namespace Shared.Orders;

public static class OrderResponse
{
    public class GetIndex
    {
        public List<OrderDto.Index> Orders { get; set; } = new();
    }

    public class GetDetail
    {
        public OrderDto.Index Order { get; set; }
    }

    public class Create
    {
        public string? OrderId { get; set; }
    }
    public class OrderNotifications
    {
        public List<NotificationDto.Index> NotificationsOrder { get; set; } = new();
    }

    public class OrderItems
    {
        public List<OrderItemDto.Index> Items { get; set; } = new();
    }
}
