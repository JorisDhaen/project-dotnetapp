using Microsoft.EntityFrameworkCore;
using Server.Models;
using Shared.OrderItem;


namespace Services.OrderItems;

public class OrderItemsService : IOrderItemService
{
    private readonly DelawareDbContext dbContext;
    private readonly DbSet<Orderitem> orderItems;

    public OrderItemsService(DelawareDbContext dbContext)
    {
        this.dbContext = dbContext;
        this.orderItems = dbContext.Orderitems;
    }

    public async Task<OrderItemResponse.GetIndex> GetIndexAsync(OrderItemRequest.GetIndex request)
    {
        OrderItemResponse.GetIndex response = new();
        var query = orderItems.AsQueryable();

        response.OrderItems = await query.Select(o => new OrderItemDto.Index
        {
            OrderItemId = o.IdorderItem,
            Quantity= o.Quantity,
            TotalPrice= o.TotalPrice,
            ProductId= o.ProductId,
            OrderId= o.OrderId,
        })
            .Where(o => o.OrderId== request.OrderId)
            .ToListAsync();

        return response;
    }
}
