using Services.Orders;
using Services.Products;
using Shared.Orders;
using Shared.Products;
using Microsoft.Extensions.DependencyInjection;
using Shared.Notifications;
using Services.Notifications;
using Microsoft.AspNetCore.Components.Authorization;

using Shared.OrderItem;
using Services.OrderItems;

namespace Services;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds all services to the DI container.
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddServices(this IServiceCollection services)
    {

        services.AddScoped<IProductService, ProductsService>();
        services.AddScoped<IOrderService, OrdersService>();
        services.AddScoped<INotificationService, NotificationService>();
        services.AddScoped<IOrderItemService, OrderItemsService>();

        return services;
    }
}

