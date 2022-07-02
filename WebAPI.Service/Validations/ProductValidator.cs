using FluentValidation;
using WebAPI.Service.Dtos;

namespace WebAPI.Service.Validations;

public class ProductValidator : AbstractValidator<ProductDto>
{
    public ProductValidator()
    {
        RuleFor(x => x.Name).NotNull().Length(2,50);
        RuleFor(x => x.UnitPrice).NotNull().ScalePrecision(5,2);
    }
}