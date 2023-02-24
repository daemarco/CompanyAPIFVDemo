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

            RuleFor(x => x.Address).NotNull().SetValidator(new AddressValidator());
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
            RuleFor(x => x.Address).NotNull().SetValidator(new AddressValidator());
        }
    }
}
