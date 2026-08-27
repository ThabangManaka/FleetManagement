using Fleet.Application.Features.Drivers.DTOs;
using MediatR;


namespace Fleet.Application.Features.Vehicles.Queries.GetDriver
{
    public record GetDriverQuery(
      Guid Id
  ) : IRequest<DriverResponse?>;
}
