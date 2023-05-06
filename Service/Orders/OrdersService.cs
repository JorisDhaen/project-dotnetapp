using Microsoft.EntityFrameworkCore;
using Server.Models;
using Shared.Orders;
using Shared.Packages;
using Shared.Notifications;
using Azure;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using System.Linq;

namespace Services.Orders;

public class OrdersService : IOrderService
{
    private readonly DelawareDbContext dbContext;
    private readonly DbSet<Order> orders;
    private readonly ClaimsPrincipal claimsPrincipal = default!;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public OrdersService(DelawareDbContext dbContext, ClaimsPrincipal claimsPrincipal, IHttpContextAccessor httpContextAccessor)
    {
        this.dbContext = dbContext;
        this.orders = dbContext.Orders;
        this.claimsPrincipal = claimsPrincipal;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<OrderResponse.Create> CreateAsync(OrderRequest.Create request)
    {
        OrderResponse.Create response = new();

        var order = new Order(request.Order.OrderId,
                                (double)request.Order.NetPrice,
                              (double)request.Order.TotalAmount,
                              request.Order.OrderDate,
                              request.Order.StatusDate,
                              request.Order.DeliveryAdress,
                              request.Order.InvoiceAdress,
                              request.Order.OrderStatus.ToString(),
                              request.Order.UserId
                              );

        order.Package = new(request.Package.PackageId
                            ,request.Package.Height,
                            request.Package.Width,
                            request.Package.Dept,
                            request.Package.Price);

        order.Notifications.Add(
            new Notification("Uw bestelling werd succesvol aangemaakt!")
            );

        foreach (var item in request.Items)
        {
            var orderItemdb = new Orderitem(item.OrderItemId, (int)item.Quantity, (int)item.TotalPrice, item.ProductId, order.Idorder);
            order.Orderitems.Add(orderItemdb);
        }

        orders.Add(order);
        await dbContext.SaveChangesAsync();

        return response;
    }

    public async Task<OrderResponse.GetDetail> GetDetailAsync(OrderRequest.GetDetail request)
    {
        OrderResponse.GetDetail response = new();
        response.Order = await dbContext.Orders.Select(o => new OrderDto.Index
        {
            OrderId = o.Idorder,
            Reference = "",
            NetPrice = o.NetPrice,
            OrderDate = o.OrderDate,
            StatusDate = o.StatusDate,
            OrderStatus = Int32.Parse(o.OrderStatus),
            DeliveryAdress = o.DeliveryAdress,
            InvoiceAdress = o.InvoiceAdress,
            UserId = o.UserId,
            Package = new PackageDto.Index
            {
                PackageId = o.PackageId,
            },
            TaxAmount = o.TaxAmount,
            TotalAmount = o.TotalAmount,
            notifications = new()
        }).SingleAsync(x => x.OrderId == request.OrderId);

        return response;
    }

    public async Task<OrderResponse.GetIndex> GetIndexAsync(OrderRequest.GetIndex request)
    {
        OrderResponse.GetIndex response = new();
        var query = dbContext.Orders.AsQueryable();

        response.Orders = await query.Select(o => new OrderDto.Index
        {
            OrderId = o.Idorder,
            Reference = "",
            NetPrice = o.NetPrice,
            OrderDate = o.OrderDate,
            StatusDate = o.StatusDate,
            OrderStatus = Int32.Parse(o.OrderStatus),
            DeliveryAdress = o.DeliveryAdress,
            InvoiceAdress = o.InvoiceAdress,
            UserId = o.UserId,
            Package = new PackageDto.Index
            {
                PackageId = o.PackageId,
            },
            TaxAmount = o.TaxAmount,
            TotalAmount = o.TotalAmount,
            notifications = new()
        }).ToListAsync();


        return response;
    }

    public async Task<OrderResponse.GetIndex> GetIndexUserAsync(OrderRequest.GetIndexUser request)
    {
        OrderResponse.GetIndex response = new();
        var query = dbContext.Orders.AsQueryable();

        // steeds null
        //var id = claimsPrincipal?.FindFirst(ClaimTypes.NameIdentifier)?.Value;


       Console.WriteLine("ID");
        Console.WriteLine(request.UserId);

        response.Orders = await query.Select(o => new OrderDto.Index
        {
            OrderId = o.Idorder,
            Reference = "",
            NetPrice = o.NetPrice,
            OrderDate = o.OrderDate,
            StatusDate = o.StatusDate,
            OrderStatus = Int32.Parse(o.OrderStatus),
            DeliveryAdress = o.DeliveryAdress,
            InvoiceAdress = o.InvoiceAdress,
            UserId = o.UserId,
            Package = new PackageDto.Index
            {
                PackageId = o.PackageId,
            },
            TaxAmount = o.TaxAmount,
            TotalAmount = o.TotalAmount,
            notifications = new()
        }).Where(o => o.UserId == request.UserId).ToListAsync();


        return response;
    }

    public async Task<OrderResponse.OrderNotifications> GetNotificationsOrderAsync(OrderRequest.GetDetail request)
    {
        OrderResponse.OrderNotifications response = new();
        var query = dbContext.Notifications.AsQueryable();

        Console.WriteLine(request.OrderId);

        query = query.Where(query => query.OrderId == request.OrderId);

        response.NotificationsOrder = await query.Select(n => new NotificationDto.Index
        {
            OrderId = n.OrderId,
            NotificationId = n.Idnotification,
            Duration = n.Duration,
            IsSeen = n.IsSeen,
            NotificationDate = n.NotificationDate,
            Message = n.Message
        }).ToListAsync();

        return response;
    }

    public async Task<OrderResponse.OrderItems> GetOrderItemsOrderAsync(OrderRequest.GetDetail request)
    {
        OrderResponse.OrderItems response = new();
        var query = dbContext.Orderitems.AsQueryable();

        Console.WriteLine(request.OrderId);

        query = query.Where(oi => oi.OrderId == request.OrderId);
        response.Items = await query.Select(n => new OrderItemDto.Index
        {
            OrderItemId = n.IdorderItem,
            OrderId= n.OrderId,
            Quantity = n.Quantity,
            TotalPrice = n.TotalPrice,
            ProductId = n.ProductId
        }).ToListAsync();

        return response;
    }

    public async Task EditAsync(string OrderId, OrderDto.Mutate model)
    {
        Order? order = await dbContext.Orders.SingleOrDefaultAsync(x => x.Idorder == OrderId);

        if (order is null) throw new Exception();

        order.OrderStatus = model.OrderStatus.ToString();
            
        switch (order.OrderStatus)
        {
            case "1":
                break;
            case "2":
                order.Notifications.Add(new Notification("Uw bestelling werd verwerkt!")); break;
            case "3":
                order.Notifications.Add(new Notification("De bezorger is onderweg")); break;
            case "4":
                order.Notifications.Add(new Notification("Uw bestelling is geleverd!")); break;
            default: break;
        }

        await dbContext.SaveChangesAsync();
    }
}
