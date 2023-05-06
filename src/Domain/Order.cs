namespace Domain;
public class Order
{
    public string? OrderId { get;}
    public double? NetPrice { get; set; }
    private double? TotalAmount { get; set; }
    public DateTime? OrderDate { get; set; }
    public DateTime? StatusDate { get; set; }
    private Address? DeliveryAddress { get; set; }
    private Address? InvoiceAddress { get; set; }
    public ISet<OrderItem> Items => _items;
    public OrderStatus _orderStatus { get; private set; }
    private ISet<OrderItem> _items = new HashSet<OrderItem>();
    //private User? _user = new();
    private Shipment? _shipment;
    public List<Notification> _notfications = new List<Notification>();

    public Order() { }

    public void ChangeStatus(OrderStatus status)
    {
        _orderStatus = status;
        _notfications.Add(new Notification(_orderStatus, OrderId, new DateTime()));
    }

    public void AddOrderItems(ISet<OrderItem> items)
    {
        _items = items;
    }

    public void RemoveOrderItem(OrderItem item)
    {
        _items.Remove(item);
    }
}
