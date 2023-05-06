namespace Shared.Notifications;

public static class NotificationResponse
{
    public class GetIndex
    {
        public List<NotificationDto.Index> Notifications { get; set; }
    }

    public class Edit
    {
        public string NotificationId { get; set; }
    }

    public class Delete
    {

    }
}
