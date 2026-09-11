using Fleet.Application.Features.Trips.DTOs;
using MediatR;


namespace Fleet.Application.Features.Commands
{
    public record UpdateTripCommand(
     Guid Id,
     UpdateTripRequest Request
 ) : IRequest;
}
