namespace Domain;
internal class User
{
    private string Name { get; set; }
    private string UserId { get; set; } 
    private string Email { get; set; }
    private Address HomeAddress { get; set; }

    private List<Supplier> _suppliers = new List<Supplier>();
    private List<Order> _orders = new List<Order>();
}
