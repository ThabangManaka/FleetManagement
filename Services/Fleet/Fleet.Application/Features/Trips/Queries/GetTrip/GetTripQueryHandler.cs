using Fleet.Application.Features.Trips.DTOs;
using Fleet.Application.Interfaces;
using MediatR;


namespace Fleet.Application.Features.Trips.Queries.GetTrip
{
    public class GetTripQueryHandler
            : IRequestHandler<GetTripQuery, TripResponse?>
    {
        private readonly ITripRepository _tripRepository;

        public GetTripQueryHandler(
            ITripRepository tripRepository)
        {
            _tripRepository = tripRepository;
        }

        public async Task<TripResponse?> Handle(
            GetTripQuery query,
            CancellationToken cancellationToken)
        {
            var trip = await _tripRepository.GetByIdAsync(
                query.Id,
                cancellationToken);

            if (trip is null)
            {
                return null;
            }

            return new TripResponse(
                trip.Id,
                trip.VehicleId,
                trip.DriverId,
                trip.StartLocation,
                trip.Destination,
                trip.StartDate,
                trip.EndDate,
                trip.StartMileage,
                trip.EndMileage,
                trip.Status,
                trip.Notes,
                trip.CreatedAt,
                trip.UpdatedAt);
        }
    }
}