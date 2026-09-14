using Fleet.Application.Features.FuelTransactions.DTOs;
using MediatR;

namespace Fleet.Application.Features.Commands
{
    public record CreateFuelTransactionCommand(
    CreateFuelTransactionRequest Request
) : IRequest<Guid>;
}