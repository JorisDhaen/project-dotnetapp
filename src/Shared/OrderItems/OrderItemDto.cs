using FluentValidation;
using Shared.Orders;
using Shared.Products;

public class OrderItemDto
{
    public class Index
    {
        public string? OrderItemId { get; set; }
        public int? Quantity { get; set; }
        public double? TotalPrice { get; set; }
        public string? ProductId { get; set; }
        public string? OrderId { get; set; }

        // Relations
        public OrderDto.Index? Order { get; set; }
        public ProductDto.Index? Product { get; set; }
    }
    public class Create
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }

        public class Validator : AbstractValidator<Create>
        {
            public Validator()
            {
                
                RuleFor(x => x.Quantity).NotEmpty().GreaterThan(0);
            }
        }
    }

    public class Mutate
    {
        public string? OrderItemId { get; set; }
        public int? Quantity { get; set; }
        public double? TotalPrice { get; set; }
        public string? ProductId { get; set; }

        public class Validator : AbstractValidator<Mutate>
        {
            public Validator()
            {
                
                RuleFor(x => x.TotalPrice).NotEmpty().GreaterThan(0);
                RuleFor(x => x.ProductId).NotEmpty();
                RuleFor(x => x.Quantity).NotEmpty().GreaterThan(0);
            }
        }
    }
}

