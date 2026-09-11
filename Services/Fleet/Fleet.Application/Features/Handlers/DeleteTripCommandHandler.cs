using Fleet.Application.Features.Commands;
using Fleet.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fleet.Application.Features.Handlers
{
    public class DeleteTripCommandHandler
         : IRequestHandler<DeleteTripCommand>
    {
        private readonly ITripRepository _tripRepository;

        public DeleteTripCommandHandler(
            ITripRepository tripRepository)
        {
            _tripRepository = tripRepository;
        }

        public async Task Handle(
            DeleteTripCommand command,
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

            _tripRepository.Delete(trip);

            await _tripRepository.SaveChangesAsync(
                cancellationToken);
        }
    }
}