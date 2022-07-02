using FluentValidation;
using WebAPI.Service.Dtos;

namespace WebAPI.Service.Validations;

public class StoreValidator : AbstractValidator<StoreDto>
{
    public StoreValidator()
    {
        RuleFor(x => x.StoreName).NotNull().Length(2,50);
        RuleFor(x => x.Address).NotNull().MinimumLength(10);
    }
}