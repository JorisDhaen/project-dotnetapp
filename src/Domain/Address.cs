namespace Domain;

public class Address
{
    private string Street { get; set; }
    private string City { get; set; }
    private string PostalCode { get; set; }
    private string Country { get; set; }

    public Address(string street, string city, string postalcode, string country)
    {
        Street = street; 
        City = city;
        PostalCode = postalcode;
        Country = country;
    }
    
}
