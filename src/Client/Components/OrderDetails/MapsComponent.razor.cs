using GoogleMapsComponents;
using GoogleMapsComponents.Maps;
using Microsoft.AspNetCore.Components;
using Shared.Orders;

namespace Client.Components.OrderDetails;
public partial class MapsComponent
{
    [Parameter, EditorRequired] public string? Id { get; set; }
    [Parameter] public EventCallback<DateTime> CalculateEstimateTime { get; set; }


    private OrderDto.Index? order;
    [Inject] public IOrderService OrderService { get; set; } = default!;

    private GoogleMap? map1;
    private MapOptions? mapOptions;
    private string? TotalTime;
    private DirectionsRenderer dirRend = default!;

    protected async override Task OnInitializedAsync()
    {
        await GetOrder();
        mapOptions = new MapOptions()
        {
            Zoom = 18,
            Center = new LatLngLiteral()
            {
                Lat = 51.01372377623678,
                Lng = 3.735355755907398
            },
            MapTypeId = MapTypeId.Roadmap,
            
        };
        await OnAfterInitAsync();
    }
    private async Task GetOrder()
    {
        OrderRequest.GetDetail request = new() { OrderId = Id };
        var response = await OrderService.GetDetailAsync(request);
        order = response.Order;
    }
    private async Task OnAfterInitAsync()
    {
        TotalTime = null;
        dirRend = await DirectionsRenderer.CreateAsync(map1!.JsRuntime, new DirectionsRendererOptions()
        {
            Map = map1.InteropObject
        });

        //Direction Request
        DirectionsRequest dr = new DirectionsRequest();
        dr.Origin = "Sluisweg 1, 9000 Gent";
        dr.Destination = order!.DeliveryAdress!;
        dr.TravelMode = TravelMode.Driving;

        //Calculate Route
        var directionsResult = await dirRend.Route(dr);
        foreach (var route in directionsResult.Routes.SelectMany(x => x.Legs))
        {
            TotalTime += route.Duration.Text;
        }
        Console.WriteLine(TotalTime);
        int uur = 0;
        int min = 0;

        var array = TotalTime!.Split();

        if (array.Length > 2)
        {
            uur = Int32.Parse(array[0]);
            min = Int32.Parse(array[2]);
        }
        else
        {
            min = Int32.Parse(array[0]);
        }

        await CalculateEstimateTime.InvokeAsync(order.StatusDate.Add(new TimeSpan(uur, min, 0)));
    }

}