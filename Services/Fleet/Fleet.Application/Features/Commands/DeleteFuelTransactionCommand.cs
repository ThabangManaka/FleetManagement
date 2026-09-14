using MediatR;

namespace Fleet.Application.Features.Commands
{
    public record DeleteFuelTransactionCommand(
    Guid Id
) : IRequest;
}