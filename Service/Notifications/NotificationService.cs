using Microsoft.EntityFrameworkCore;
using Persistence;
using Server.Models;
using Shared.Notifications;
using Shared.Orders;

namespace Services.Notifications;

public class NotificationService : INotificationService
{
    private readonly DelawareDbContext dbContext;
    private readonly DbSet<Notification> notifications;

    public NotificationService(DelawareDbContext dbContext)
    {
        this.dbContext = dbContext;
        this.notifications = dbContext.Notifications;
    }

    public async Task DeleteAsync(NotificationRequest.Delete request)
    {
        dbContext.Notifications.RemoveIf(n => n.Idnotification == request.NotificationId);
        await dbContext.SaveChangesAsync();
    }

    public async Task<NotificationResponse.Edit> EditAsync(NotificationRequest.Edit request)
    {
        NotificationResponse.Edit response = new();

        var notification = await notifications.SingleAsync(x => x.Idnotification == request.NotificationId);

        var order = await dbContext.Orders.SingleAsync(x => x.Idorder == request.Notification.OrderId);

        var model = request.Notification;
        notification.Idnotification = model.NotificationId;
        notification.Message = model.Message;
        notification.IsSeen = (short)model.IsSeen;
        notification.Duration = model.Duration;
        notification.NotificationDate = model.NotificationDate;
        notification.OrderId = model.OrderId;
        notification.Order = order;

        await dbContext.SaveChangesAsync();
        response.NotificationId = notification.Idnotification;

        return response;
    }

    public async Task<NotificationResponse.GetIndex> GetIndexAsync(NotificationRequest.GetIndex request)
    {
        NotificationResponse.GetIndex response = new();
        var query = notifications.AsQueryable();

        response.Notifications = await query.Select(n => new NotificationDto.Index
        {
            NotificationId = n.Idnotification,
            Message = n.Message,
            Order = new OrderDto.Index
            {
                OrderId = n.OrderId,
            },
            OrderId = n.OrderId,
            NotificationDate = n.NotificationDate,
            Duration = n.Duration,
            IsSeen = n.IsSeen,
        }).ToListAsync();

        return response;
    }

    public async Task<NotificationResponse.GetIndex> GetIndexWithIsSeenEqualsZeroAsync(NotificationRequest.GetIndex request)
    {
        NotificationResponse.GetIndex response = new();
        var query = notifications.AsQueryable();

        response.Notifications = await query.Select(n => new NotificationDto.Index
        {
            NotificationId = n.Idnotification,
            Message = n.Message,
            Order = new OrderDto.Index
            {
                OrderId = n.OrderId,
            },
            OrderId = n.OrderId,
            NotificationDate = n.NotificationDate,
            Duration = n.Duration,
            IsSeen = n.IsSeen,
        })
            .Where(x => x.IsSeen == 0)
            .ToListAsync();

        return response;
    }
}
