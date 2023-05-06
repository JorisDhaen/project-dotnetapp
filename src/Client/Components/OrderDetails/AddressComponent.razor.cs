using Microsoft.AspNetCore.Components;

namespace Client.Components.OrderDetails;

public partial class AddressComponent
{
    [Parameter, EditorRequired] public string? Address { get; set; }


}