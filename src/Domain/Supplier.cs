namespace Domain;
public class Supplier
{
    private string SupplierId { get; set; }
    private string SupplierName { get; set; }
    private Address SupplierAddress { get; set; }

    private List<Product> _products = new List<Product>();
}