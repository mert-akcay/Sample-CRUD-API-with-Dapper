using FluentValidation;
using WebAPI.Service.Dtos;

namespace WebAPI.Service.Validations;

public class CustomerValidator : AbstractValidator<CustomerDto>
{
    public CustomerValidator()
    {
        RuleFor(x => x.FirstName).NotNull().Length(2,50);
        RuleFor(x => x.LastName).NotNull().Length(2,50);
        RuleFor(x => x.Email).NotNull().NotEmpty().EmailAddress();
        RuleFor(x => x.Phone).NotNull().NotEmpty().Length(10);
    }
}