using Fleet.Application.Features.Drivers.DTOs;
using FluentValidation;


namespace Fleet.Application.Features.Drivers.Validators
{
    public class CreateDriverValidator : AbstractValidator<CreateDriverRequest>
    {
        public CreateDriverValidator()
        {
            RuleFor(x => x.EmployeeNumber)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(x => x.FirstName)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.LastName)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress()
                .MaximumLength(200);

            RuleFor(x => x.PhoneNumber)
                .NotEmpty()
                .MaximumLength(20);

            RuleFor(x => x.LicenseNumber)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(x => x.LicenseExpiryDate)
                .GreaterThan(DateTime.UtcNow.Date)
                .WithMessage("License expiry date must be in the future.");
        }
    }
}