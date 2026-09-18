using Fleet.Application.Features.Commands;
using FluentValidation;


namespace Fleet.Application.Features.Maintenance.Validators
{
    public class CreateMaintenanceCommandValidator
         : AbstractValidator<CreateMaintenanceCommand>
    {
        public CreateMaintenanceCommandValidator()
        {
            RuleFor(x => x.Request.VehicleId)
                .NotEmpty()
                .WithMessage("Vehicle is required.");

            RuleFor(x => x.Request.MaintenanceType)
                .NotEmpty()
                .WithMessage("Maintenance type is required.")
                .MaximumLength(100)
                .WithMessage("Maintenance type cannot exceed 100 characters.");

            RuleFor(x => x.Request.Description)
                .NotEmpty()
                .WithMessage("Description is required.")
                .MaximumLength(500)
                .WithMessage("Description cannot exceed 500 characters.");

            RuleFor(x => x.Request.ServiceDate)
                .NotEmpty()
                .WithMessage("Service date is required.");

            RuleFor(x => x.Request.Mileage)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Mileage cannot be negative.");

            RuleFor(x => x.Request.Cost)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Cost cannot be negative.");

            RuleFor(x => x.Request.Notes)
                .MaximumLength(1000)
                .When(x => !string.IsNullOrWhiteSpace(x.Request.Notes))
                .WithMessage("Notes cannot exceed 1000 characters.");
        }
    }
}
