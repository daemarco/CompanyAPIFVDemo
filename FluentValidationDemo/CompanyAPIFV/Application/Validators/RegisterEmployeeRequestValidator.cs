﻿using CompanyAPIFV.Application.Contracts;
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
                .Must(x => x?.Length >= 1 && x.Length <= 3)
                .WithMessage("The number of addresses must be between 1 and 3")
                .ForEach(x =>
                {
                    x.NotNull();
                    x.SetValidator(new AddressValidator());
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
