namespace Domain;
public class OrderItem
{
    public string ItemId { get; set; }
    public int Quantity { get; set; }
    public double TotalPrice { get; set; }

    public Product Product { get; set; }

    public OrderItem(string itemId, int quantity, double totalPrice, Product product)
    {
        ItemId = itemId;
        Quantity = quantity;
        TotalPrice = totalPrice;
        Product = product;
    }
}