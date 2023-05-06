

using Microsoft.AspNetCore.Components;
using Shared.Orders;

namespace Client.Components.OrderDetails
{
    public partial class OrderInfoComponent
    {
        [Parameter] public int? OrderStatus { get; set; }
        [Parameter] public DateTime? ArrivalTime { get; set; } 
        [Parameter] public DateTime? StatusDate { get; set; }
        [Parameter, EditorRequired] public string? Address { get; set; }
        [Inject] public IOrderService OrderService { get; set; } = default!;
    }
}