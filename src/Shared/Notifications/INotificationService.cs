namespace Shared.Notifications;

public interface INotificationService
{
    // GEBRUIKT DOOR REAL SERVICE --> HAALT DUS DATA UIT DATABANK
    Task<NotificationResponse.GetIndex> GetIndexAsync(NotificationRequest.GetIndex request);
    Task<NotificationResponse.Edit> EditAsync(NotificationRequest.Edit request);
    Task<NotificationResponse.GetIndex> GetIndexWithIsSeenEqualsZeroAsync(NotificationRequest.GetIndex request);
    Task DeleteAsync(NotificationRequest.Delete request);
}
