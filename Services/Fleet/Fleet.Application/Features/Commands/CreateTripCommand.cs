using Fleet.Application.Features.Trips.DTOs;
using MediatR;


namespace Fleet.Application.Features.Commands
{
    public record CreateTripCommand(
        CreateTripRequest Request
    ) : IRequest<Guid>;
}
