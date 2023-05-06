using Shared.Notifications;
using System.Net.Http.Json;

namespace Client.Notifications;

public class NotificationService : INotificationService
{
    private readonly HttpClient client;
    private const string endpoint = "api/notification";

    public NotificationService(HttpClient client)
    {
        this.client = client;
    }

    public async Task<NotificationResponse.GetIndex> GetIndexAsync(NotificationRequest.GetIndex request)
    {
        var response = await client.GetFromJsonAsync<NotificationResponse.GetIndex>($"{endpoint}");
        return response;
    }

    public async Task<NotificationResponse.Edit> EditAsync(NotificationRequest.Edit request)
    {
        var response = await client.PutAsJsonAsync(endpoint, request);
        return await response.Content.ReadFromJsonAsync<NotificationResponse.Edit>();
    }

    public async Task<NotificationResponse.GetIndex> GetIndexWithIsSeenEqualsZeroAsync(NotificationRequest.GetIndex request)
    {
        var response = await client.GetFromJsonAsync<NotificationResponse.GetIndex>($"{endpoint}/IsSeen");
        return response;
    }

    public async Task DeleteAsync(NotificationRequest.Delete request)
    {
        await client.DeleteAsync($"{endpoint}/{request.NotificationId}");
    }
}
