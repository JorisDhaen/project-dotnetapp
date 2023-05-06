using Microsoft.AspNetCore.Components;
using Shared.Orders;

namespace Client.Components.OrderDetails;

public partial class StatusComponent
{
    [Parameter, EditorRequired] public int? OrderStatus { get; set; }
    [Inject] public IOrderService OrderService { get; set; } = default!;
}