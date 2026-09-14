using Fleet.Application.Features.Commands;
using Fleet.Application.Interfaces;
using MediatR;

namespace Fleet.Application.Features.Handlers
{
    public class UpdateTripCommandHandler
          : IRequestHandler<UpdateTripCommand>
    {
        private readonly ITripRepository _tripRepository;

        public UpdateTripCommandHandler(
            ITripRepository tripRepository)
        {
            _tripRepository = tripRepository;
        }

        public async Task Handle(
            UpdateTripCommand command,
            CancellationToken cancellationToken)
        {
            var trip = await _tripRepository.GetByIdAsync(
                command.Id,
                cancellationToken);

            if (trip is null)
            {
                throw new KeyNotFoundException(
                    $"Trip with ID '{command.Id}' was not found.");
            }

            trip.UpdateDetails(
                command.Request.StartLocation,
                command.Request.Destination,
                command.Request.StartDate,
                command.Request.EndDate,
                command.Request.StartMileage,
                command.Request.EndMileage,
                command.Request.Status,
                command.Request.Notes);

            _tripRepository.Update(trip);

            await _tripRepository.SaveChangesAsync(
                cancellationToken);
        }
    }
}
