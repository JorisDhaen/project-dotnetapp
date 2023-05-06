namespace Domain;
public class Shipment
{
    private string ShipmentId { get; set; }
    private DateTime EstamatedArrivalTime { get; set; }
    private DateTime DepartureTime { get; set; }
    private int ShipmentStatus { get; set; }
    private Address DeliveryAddress { get; set; }
    private Address DepartureAddress { get; set; }

    private List<Order> _orders = new();

}
