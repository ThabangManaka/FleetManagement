using Fleet.Application.Features.FuelTransactions.DTOs;
using MediatR;

namespace Fleet.Application.Features.Commands
{
    public record UpdateFuelTransactionCommand(
    Guid Id,
    UpdateFuelTransactionRequest Request
) : IRequest;
}