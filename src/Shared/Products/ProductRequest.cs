namespace Shared.Products;

public class ProductRequest
{
    public class GetIndex
    {

    }

    public class GetDetail
    {
        public string? ProductId { get; set; }
    }
}
