using Fleet.Application.Features.Vehicles.DTOs;
using MediatR;


namespace Fleet.Application.Features.Vehicles.Queries.GetVehicle
{

    public record GetVehicleQuery(Guid Id)
        : IRequest<VehicleResponse?>;
}
