using Client.Products;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sql;
using Shared.Notifications;
using Swashbuckle.AspNetCore.Annotations;

namespace Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotificationController : Controller
    {
        private readonly INotificationService notificationService;

        public NotificationController(INotificationService notificationService)
        {
            this.notificationService = notificationService;
        }

        [SwaggerOperation("Returns a list of Notifications")]
        [HttpGet]
        public Task<NotificationResponse.GetIndex> GetIndex([FromQuery] NotificationRequest.GetIndex request)
        {
            return notificationService.GetIndexAsync(request);
        }

        [SwaggerOperation("Update a notification")]
        [HttpPut]
        public Task<NotificationResponse.Edit> EditAsync([FromBody] NotificationRequest.Edit request)
        {
            return notificationService.EditAsync(request);
        }

        [SwaggerOperation("Returns a list of notifications not yet seen by user")]
        [HttpGet("IsSeen")]
        public Task<NotificationResponse.GetIndex> GetIndexWithIsSeenEqualsZero([FromQuery] NotificationRequest.GetIndex request)
        {
            return notificationService.GetIndexWithIsSeenEqualsZeroAsync(request);
        }

        [SwaggerOperation("Deletes an existing notification.")]
        [HttpDelete("{notificationId}")]
        public async Task Delete([FromRoute] NotificationRequest.Delete request)
        {
            await notificationService.DeleteAsync(request);
        }

    }
}
