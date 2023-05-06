// See https://aka.ms/new-console-template for more information
using Domain;
using Shared.Orders;
using System.Net.Http.Json;

HttpClient client =new HttpClient();
const string endpoint = "http://localhost:5056/api/order";

Console.WriteLine("WIJZIG STATUS BESTELLING");
Console.WriteLine("Geef nummer bestelling: ");

string bestelNr = Console.ReadLine();
Console.WriteLine();

//TODO Juiste order ophalen DB indien order bestaat
var response = await client.GetFromJsonAsync<OrderResponse.GetDetail>($"{endpoint}/{bestelNr}");

int status = 0;
do
{
    Console.WriteLine($"Wijzig status voor bestelling: {bestelNr}");
    Console.WriteLine("CREATED = 1");
    Console.WriteLine("PROCCESSED = 2");
    Console.WriteLine("SHIPPED = 3");
    Console.WriteLine("DELIVERED = 4");
    Console.WriteLine("Geef nummer: ");

    try
    {
        status = int.Parse(Console.ReadLine());
    } catch {
        Console.WriteLine("Geef een getal tussen 1 en 4 in ");
    }

} while (status < 1 || status > 4);

switch (status)
{
    case 1:
        response.Order.OrderStatus = 1; break;
    case 2:
        response.Order.OrderStatus = 2; break;
    case 3:
        response.Order.OrderStatus = 3; break;
    case 4:
        response.Order.OrderStatus = 4; break;
    default: break;
}

OrderDto.Mutate model = new()
{
    OrderId = response.Order.OrderId,
    OrderStatus = response.Order.OrderStatus,
    OrderDate = response.Order.OrderDate,
    DeliveryAdress = response.Order.DeliveryAdress,
    InvoiceAdress = response.Order.InvoiceAdress,
    NetPrice = response.Order.NetPrice,
    StatusDate = response.Order.StatusDate,
    TaxAmount = response.Order.TaxAmount,
    TotalAmount = response.Order.TotalAmount,
    UserId = response.Order.UserId,
};

await client.PutAsJsonAsync($"{endpoint}/{bestelNr}", model);

Console.WriteLine();
Console.WriteLine($"STATUS GEWIJZIGD NAAR {response.Order.OrderStatus}");

response.Order.notifications.ForEach(noti => Console.WriteLine(noti.Message));
