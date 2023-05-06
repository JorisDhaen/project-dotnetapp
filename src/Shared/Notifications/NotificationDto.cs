using Shared.Orders;

namespace Shared.Notifications;

public class NotificationDto
{
    public class Index
    {
        public string? NotificationId { get; set; }
        public string? Message { get; set; }
        public string? OrderId { get; set; }
        public DateTime NotificationDate { get; set; }
        public int? Duration { get; set; }
        public int? IsSeen { get; set; }

        // Relation
        public OrderDto.Index? Order { get; set; }


    }

    public class Mutate
    {
        public string? NotificationId { get; set; }
        public string? Message { get; set; }
        public string? OrderId { get; set; }
        public DateTime NotificationDate { get; set; }
        public int? Duration { get; set; }
        public int? IsSeen { get; set; }
    }
}
