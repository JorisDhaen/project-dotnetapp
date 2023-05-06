namespace Domain;
public class Product
{
    #region Fields
    #endregion

    #region Properties
    public string ProductId { get; set; }
    public string ProductName { get; set; }
    public double UnitPrice { get; set; }
    public int LeftInStock { get; set; }
    public string Description { get; set; }
    public double Height { get; set; }
    public double Width { get; set; }
    public double Depth { get; set; }
    #endregion Properties
    #region Constructors

    public Product(string productId, string productName, string description, double unitPrice, int leftInStock, double height, double width, double depth)
    {
        ProductId = productId;
        ProductName = productName;
        UnitPrice = unitPrice;
        LeftInStock = leftInStock;
        Description = description;
        Height = height;
        Width = width;
        Depth = depth;

    }
    #endregion
}
