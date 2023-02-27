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

            RuleFor(x => x.Addresses).NotNull().SetValidator(new AddressesValidator());
        }
    }

    public class AddressesValidator : AbstractValidator<AddressDto[]>
    {
        public AddressesValidator()
        {
            RuleFor(x => x).NotNull()
                .ListMustContainsNumberOfItems(1, 3)
                .ForEach(x =>
                {
                    x.NotNull();
                    x.SetValidator(new AddressValidator());
                });
        }
    }

    public static class CustomValidators
    {
        public static IRuleBuilderOptionsConditions<T, IList<TElement>> ListMustContainsNumberOfItems<T, TElement>(
            this IRuleBuilder<T, IList<TElement>> ruleBuilder, int? min = null, int? max = null)
        {
            return ruleBuilder.Custom((list, context) =>
            {
                if (min.HasValue && list.Count < min.Value)
                {
                    context.AddFailure($"The list must contain {min.Value} items at least. It contains {list.Count} items.");
                }

                if (max.HasValue && list.Count > max.Value)
                {
                    context.AddFailure($"The list must contain {max.Value} items maximum. It contains {list.Count} items.");
                }
            });
        }
    }

    public class AddressValidator : AbstractValidator<AddressDto>
    {
        public AddressValidator()
        {
            RuleFor(x => x.Street).NotEmpty().Length(0, 100);
            RuleFor(x => x.City).NotEmpty().Length(0, 40);
            RuleFor(x => x.State).NotEmpty().Length(0, 2);
            RuleFor(x => x.ZipCode).NotEmpty().Length(0, 5);
        }
    }

    public class EditPersonalInformationRequestValidator : AbstractValidator<EditPersonalInformationRequest> 
    {
        public EditPersonalInformationRequestValidator()
        {
            RuleFor(x => x.Name).NotEmpty().Length(0, 200);

            RuleFor(x => x.Addresses).NotNull()
                .SetValidator(new AddressesValidator());
        }
    }
}
