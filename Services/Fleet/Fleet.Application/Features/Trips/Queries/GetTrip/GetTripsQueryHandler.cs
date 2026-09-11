using Fleet.Application.Features.Trips.DTOs;
using Fleet.Application.Interfaces;
using MediatR;


namespace Fleet.Application.Features.Trips.Queries.GetTrip
{
    public class GetTripsQueryHandler
     : IRequestHandler<GetTripsQuery, List<TripResponse>>
    {
        private readonly ITripRepository _tripRepository;

        public GetTripsQueryHandler(
            ITripRepository tripRepository)
        {
            _tripRepository = tripRepository;
        }

        public async Task<List<TripResponse>> Handle(
            GetTripsQuery query,
            CancellationToken cancellationToken)
        {
            var trips = await _tripRepository.GetAllAsync(
                cancellationToken);

            return trips
                .Select(trip => new TripResponse(
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
                    trip.UpdatedAt))
                .ToList();
        }
    }
}