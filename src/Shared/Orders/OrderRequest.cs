using FluentValidation;
using Shared.Packages;

namespace Shared.Orders;

public static class OrderRequest
{
    public class GetIndex
    {
       
    }


    public class GetIndexUser
    {
        public string? UserId { get; set; }
    }
    public class Validator : AbstractValidator<GetDetail>
    {
        public Validator()
        {

            RuleFor(x => x.OrderId).NotEmpty();
   

        }
    }

    public class GetDetail
    {
        public string? OrderId { get; set; }
     

        public class Validator : AbstractValidator<GetDetail>
        {
            public Validator()
            {

                RuleFor(x => x.OrderId).NotEmpty();

            }
        }
    }

    public class Create
    {
        public OrderDto.Mutate? Order { get; set; }
        public PackageDto.Mutate? Package { get; set; }
        public List<OrderItemDto.Mutate>? Items { get; set; }


        public class Validator : AbstractValidator<Create>
        {
            public Validator()
            {

                RuleFor(x => x.Order).NotEmpty().SetValidator(new OrderDto.Mutate.Validator());
                RuleFor(x => x.Package).NotEmpty().SetValidator(new PackageDto.Mutate.Validator());
                RuleForEach(x => x.Items).NotEmpty().SetValidator(new OrderItemDto.Mutate.Validator());
            }
        }
    }
}
