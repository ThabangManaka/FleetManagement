using Fleet.Application.Features.Commands;
using FluentValidation;


namespace Fleet.Application.Features.FuelTransactions.Validators
{
    public class UpdateFuelTransactionCommandValidator
        : AbstractValidator<UpdateFuelTransactionCommand>
    {
        public UpdateFuelTransactionCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Fuel transaction ID is required.");

            RuleFor(x => x.Request.TransactionDate)
                .NotEmpty()
                .WithMessage("Transaction date is required.");

            RuleFor(x => x.Request.Mileage)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Mileage cannot be negative.");

            RuleFor(x => x.Request.Litres)
                .GreaterThan(0)
                .WithMessage("Litres must be greater than zero.");

            RuleFor(x => x.Request.PricePerLitre)
                .GreaterThan(0)
                .WithMessage("Price per litre must be greater than zero.");

            RuleFor(x => x.Request.FuelType)
                .NotEmpty()
                .WithMessage("Fuel type is required.")
                .MaximumLength(50)
                .WithMessage("Fuel type cannot exceed 50 characters.");

            RuleFor(x => x.Request.FuelStation)
                .MaximumLength(200)
                .When(x => x.Request.FuelStation != null)
                .WithMessage("Fuel station cannot exceed 200 characters.");

            RuleFor(x => x.Request.ReceiptNumber)
                .MaximumLength(100)
                .When(x => x.Request.ReceiptNumber != null)
                .WithMessage("Receipt number cannot exceed 100 characters.");

            RuleFor(x => x.Request.Notes)
                .MaximumLength(1000)
                .When(x => x.Request.Notes != null)
                .WithMessage("Notes cannot exceed 1000 characters.");
        }
    }
}
