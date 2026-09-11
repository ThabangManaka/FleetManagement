using Fleet.Application.Features.Trips.DTOs;
using MediatR;


namespace Fleet.Application.Features.Trips.Queries.GetTrip
{
    public record GetTripQuery(
         Guid Id
     ) : IRequest<TripResponse?>;
}
