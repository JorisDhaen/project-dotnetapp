namespace Domain;
public class Package
{
    private string PackageId { get; set; }
    private double Height { get; set; }
    private double Width { get; set; }
    private double Price { get; }
    private bool Feasibility { get;}

    private OrderItem _item;
    public Package(double price, bool feasibility) : this(1, 1, price, feasibility) { }

    public Package(double height, double width, double price, bool feasibility)
    {
        Height = height;
        Width = width;
        Price = price;
        Feasibility = feasibility;
    }
}
