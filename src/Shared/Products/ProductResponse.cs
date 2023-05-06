namespace Shared.Products;

public class ProductResponse
{
    public class GetIndex
    {
        public List<ProductDto.Index> Products { get; set; } = new();
    }

    public class GetDetail
    {
        public ProductDto.Index Product { get; set; } = new();
    }
}
