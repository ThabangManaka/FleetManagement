using Fleet.Application.Features.Trips.DTOs;
using MediatR;


namespace Fleet.Application.Features.Trips.Queries.GetTrip
{
    public record GetTripsQuery
      : IRequest<List<TripResponse>>;
}
