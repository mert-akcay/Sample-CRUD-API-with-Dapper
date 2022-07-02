using FluentValidation;
using WebAPI.Service.Dtos;

namespace WebAPI.Service.Validations;

public class OrderRepository : AbstractValidator<OrderDto>
{
    public OrderRepository()
    {
        RuleFor(x => x.StoreId).NotNull();
        RuleFor(x => x.CustomerId).NotNull();
    }
}