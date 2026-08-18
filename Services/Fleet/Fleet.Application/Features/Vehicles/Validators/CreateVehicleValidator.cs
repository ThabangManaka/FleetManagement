

using Fleet.Application.Features.Vehicles.DTOs;
using Fleet.Core.Enums;
using FluentValidation;

namespace Fleet.Application.Features.Vehicles.Validators
{
    public class CreateVehicleValidator : AbstractValidator<CreateVehicleRequest>
    {
        public CreateVehicleValidator()
        {

            RuleFor(x => x.RegistrationNumber)
                  .NotEmpty()
                  .MaximumLength(20);

            RuleFor(x => x.Vin)
                  .NotEmpty()
                  .Length(17);

            RuleFor(x => x.Make)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.Model)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.Year)
                .InclusiveBetween(1900, DateTime.UtcNow.Year + 1);

            RuleFor(x => x.FuelType)
                .Must(value => Enum.IsDefined(typeof(FuelType), value))
                .WithMessage("Invalid fuel type.");
        }


    }

}
