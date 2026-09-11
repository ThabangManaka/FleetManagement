using FluentValidation;


namespace Fleet.Application.Features.Trips.Validators
{
    public class CreateTripCommandValidator
            : AbstractValidator<Commands.CreateTripCommand>
    {
        public CreateTripCommandValidator()
        {
            RuleFor(x => x.Request.VehicleId)
                .NotEmpty()
                .WithMessage("Vehicle ID is required.");

            RuleFor(x => x.Request.DriverId)
                .NotEmpty()
                .WithMessage("Driver ID is required.");

            RuleFor(x => x.Request.StartLocation)
                .NotEmpty()
                .WithMessage("Start location is required.")
                .MaximumLength(200)
                .WithMessage("Start location cannot exceed 200 characters.");

            RuleFor(x => x.Request.Destination)
                .NotEmpty()
                .WithMessage("Destination is required.")
                .MaximumLength(200)
                .WithMessage("Destination cannot exceed 200 characters.");

            RuleFor(x => x.Request.StartDate)
                .NotEmpty()
                .WithMessage("Start date is required.");

            RuleFor(x => x.Request.StartMileage)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Start mileage cannot be negative.");

            RuleFor(x => x.Request.Notes)
                .MaximumLength(1000)
                .When(x => x.Request.Notes != null)
                .WithMessage("Notes cannot exceed 1000 characters.");
        }
    }
}