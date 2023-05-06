using Microsoft.AspNetCore.Components;

namespace Client.Pages;

public partial class PackageShipment
{
    [Inject]
    public NavigationManager NavigationManager { get; set; } = default!;
    private void ContinueShopping()
    {
        NavigationManager.NavigateTo("/products");
    }
}