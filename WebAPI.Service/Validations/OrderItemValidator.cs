using FluentValidation;
using WebAPI.Service.Dtos;

namespace WebAPI.Service.Validations;

public class OrderItemValidator : AbstractValidator<OrderItemDto>
{
    public OrderItemValidator()
    {
        RuleFor(x => x.OrderId).NotNull();
        RuleFor(x => x.ProductId).NotNull();
        RuleFor(x => x.Quantity).NotNull();
    }
}