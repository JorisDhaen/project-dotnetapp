namespace Shared.OrderItem; 

public static class OrderItemResponse 
{
    public class GetIndex
    {
        public List<OrderItemDto.Index> OrderItems { get; set; } = new();
    }
}
