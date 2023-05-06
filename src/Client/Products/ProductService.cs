using Shared.Orders;
using Shared.Products;
using System.Net.Http.Json;

namespace Client.Products;

public class ProductService : IProductService
{
    private readonly HttpClient client;
    private const string endpoint = "api/product";

    public ProductService(HttpClient client)
    {
        this.client = client;
    }
    public async Task<ProductResponse.GetIndex> GetIndexAsync(ProductRequest.GetIndex request)
    {
        var response = await client.GetFromJsonAsync<ProductResponse.GetIndex>($"{endpoint}");
        return response;
    }

    public async Task<ProductResponse.GetDetail> GetDetailAsync(ProductRequest.GetDetail request)
    {
        var response = await client.GetFromJsonAsync<ProductResponse.GetDetail>($"{endpoint}/{request.ProductId}");
        return response;
    }
}
