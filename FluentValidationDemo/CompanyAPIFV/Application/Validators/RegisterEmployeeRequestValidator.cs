using CompanyAPIFV.Application.Contracts;
using FluentValidation;

namespace CompanyAPIFV.Application.Validators
{
    public class RegisterEmployeeRequestValidator : AbstractValidator<RegisterEmployeeRequest>
    {
        public RegisterEmployeeRequestValidator()
        {
            RuleFor(x => x.Name).NotEmpty().Length(0, 200);
            RuleFor(x => x.Email).NotEmpty().Length(0, 150).EmailAddress();
            RuleFor(x => x.Address).NotEmpty().Length(0, 150);
        }
    }
}
