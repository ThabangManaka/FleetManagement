using MediatR;

namespace Fleet.Application.Features.Commands
{
    public record DeleteTripCommand(
    Guid Id
) : IRequest;
}