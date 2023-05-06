using Domain;
using Microsoft.AspNetCore.Components;
using Client.Components.Products;
using Shared.Products;

namespace Client.Pages;

public partial class Products
{
    private IEnumerable<ProductDto.Index>? _products;
    [Inject] public IProductService? ProductService { get; set; }
    protected override async Task OnInitializedAsync()
    {
        await GetProducts();
        //_products = await ProductService.GetIndexAsync();
    }

    private async Task GetProducts()
    {
        ProductRequest.GetIndex request = new();
        var response = await ProductService.GetIndexAsync(request);
        _products= response.Products;
    }
}