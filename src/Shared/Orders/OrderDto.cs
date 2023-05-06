namespace Shared.Orders;
using Domain;
using FluentValidation;
using Shared.Packages;
using Shared.Products;

public static class OrderDto
{
    public class Index
    {
        public string? OrderId { get; set; }
        public string? Reference { get; set; }
        public double? NetPrice { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime StatusDate { get; set; }
        public int OrderStatus { get; set; }
        public string? DeliveryAdress { get; set; }
        public ProductDto.Index Product { get; set; }
        public string? InvoiceAdress { get; set; } 
        public string? UserId { get; set; }
        public int? TaxAmount { get; set; }
        public double? TotalAmount { get; set; }

        public List<Notification> notifications = new List<Notification>();

        // Relations
        public PackageDto.Index? Package { get; set; }
    }

    public class Create
    {
        public string? Reference { get; set; }

        public List<Notification> notifications = new List<Notification>();
        public IEnumerable<OrderItemDto.Create> Items { get; set; } = default!;
        public IEnumerable<PackageDto.Create> Packages { get; set; } = default!;

        public class Validator : AbstractValidator<Create>
        {
            public Validator()
            {

                RuleForEach(x => x.Items).NotEmpty().SetValidator(new OrderItemDto.Create.Validator());
                RuleForEach(x => x.Packages).NotEmpty().SetValidator(new PackageDto.Create.Validator());
            }
        }
    }

    public class Mutate
    {
        public string? OrderId { get; set; }
        public double? NetPrice { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime StatusDate { get; set; }
        public int OrderStatus { get; set; }
        public string? DeliveryAdress { get; set; }
        public string? InvoiceAdress { get; set; }
        public string? UserId { get; set; }
        public int? TaxAmount { get; set; }
        public double? TotalAmount { get; set; }


        public class Validator : AbstractValidator<Mutate>
        {
            public Validator()
            {
                
                    
                    RuleFor(x => x.NetPrice).NotEmpty().GreaterThan(0);
                    RuleFor(x => x.OrderDate).NotEmpty();
                    RuleFor(x => x.StatusDate).NotEmpty();
                    RuleFor(x => x.DeliveryAdress).NotEmpty();
                    RuleFor(x => x.InvoiceAdress).NotEmpty();
                RuleFor(x => x.OrderStatus).NotEmpty();
                RuleFor(x => x.TotalAmount).NotEmpty().GreaterThan(0);


            }
        }
    }
}
