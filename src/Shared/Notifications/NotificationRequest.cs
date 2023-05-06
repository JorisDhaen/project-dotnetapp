namespace Shared.Notifications;

public static class NotificationRequest
{
    public class GetIndex
    {

    }

    public class Edit
    {
        public string NotificationId { get; set; }
        public NotificationDto.Mutate? Notification { get; set; }
    }

    public class Delete
    {
        public string NotificationId { get; set; }
    }
}
