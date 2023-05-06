using Microsoft.AspNetCore.Components;
using Shared.Products;
using Domain;
using Client.Components.Products;

namespace Client.Components.Products
{
    public partial class ProductDetail
    {
        [Inject] public ShoppingCartState ShoppingCart { get; set; } = default!;
        [Parameter] public ProductDto.Index Product { get; set; }
        int amount = 1;
        public bool DetailsShown { get; set; } = false;

        private void ShowDetails()
        {
            DetailsShown = !DetailsShown;
            StateHasChanged();
        }
        private async void AddProduct()
        {
            Product product = new(Product.ProductId, Product.Name, Product.Description, Product.UnitPrice, Product.LeftInStock, Product.Height, Product.Width, Product.Depth);
            OrderItem cartItem = new(Product.ProductId, amount, Product.UnitPrice * amount, product);

            ShoppingCart.AddItem(cartItem);

            foreach (var v in ShoppingCart.Items)
            {
                await localStore.SetItemAsync(v.Product.ProductId, v);
            } 
        }
    }
}
