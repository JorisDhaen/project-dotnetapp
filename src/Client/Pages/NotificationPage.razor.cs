using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Shared.Notifications;


namespace Client.Pages;
public partial class NotificationPage
{
    [Parameter] public string NotificationId { get; set; }
    private NotificationDto.Mutate model = new();
    [Inject] public INotificationService? NotificationService { get; set; }
    private IEnumerable<NotificationDto.Index>? notifications;

    protected override async Task OnInitializedAsync()
    {
        NotificationRequest.GetIndex request = new();
        var response = await NotificationService.GetIndexAsync(request);
        notifications = response.Notifications;
    }

    private void setModel(NotificationDto.Index index)
    {
        NotificationId = index.NotificationId;
        model.NotificationId = index.NotificationId;
        model.Message= index.Message;
        model.OrderId= index.Order.OrderId;
        model.NotificationDate = index.NotificationDate;
        model.Duration= index.Duration;
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
        await NotificationService.EditAsync(request);
    }

    private async Task DeleteNotification(NotificationDto.Index index)
    {
        NotificationRequest.Delete request = new()
        {
            NotificationId = index.NotificationId,
        };
        await NotificationService.DeleteAsync(request);

        NotificationRequest.GetIndex request2 = new();
        var response = await NotificationService.GetIndexAsync(request2);
        notifications = response.Notifications;
    }
}