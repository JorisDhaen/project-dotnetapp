using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Client;
using Client.Components.Products;
using Client.Orders;
using Client.Products;
using Shared.Orders;
using Shared.Products;
using Append.Blazor.Sidepanel;
using Shared.Notifications;
using Client.Notifications;
using Microsoft.AspNetCore.Components.Authorization;

using Client.Authorisation;

using Shared.OrderItem;
using Client.OrderItems;
using Blazored.LocalStorage;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

internal class Program
{
    private static async Task Main(string[] args)
    {
        var builder = WebAssemblyHostBuilder.CreateDefault(args);
        builder.RootComponents.Add<App>("#app");
        builder.RootComponents.Add<HeadOutlet>("head::after");


        builder.Services.AddAuthorizationCore();
        builder.Services.AddScoped<FakeAuthenticationProvider>();
        builder.Services.AddScoped<AuthenticationStateProvider>(provider => provider.GetRequiredService<FakeAuthenticationProvider>());
        builder.Services.AddTransient<FakeAuthorizationMessageHandler>();


        builder.Services.AddHttpClient("Project.ServerAPI", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
              .AddHttpMessageHandler<FakeAuthorizationMessageHandler>(); ;

        builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("Project.ServerAPI"));

        builder.Services.AddScoped<IProductService, ProductService>();
        builder.Services.AddScoped<IOrderService, OrderService>();
        builder.Services.AddScoped<INotificationService, NotificationService>();
        builder.Services.AddScoped<IOrderItemService, OrderItemService>();



      




        builder.Services.AddBlazoredLocalStorage();

        builder.Services.AddSidepanel();
        builder.Services.AddSingleton<ShoppingCartState>();

        await builder.Build().RunAsync();
    }
}