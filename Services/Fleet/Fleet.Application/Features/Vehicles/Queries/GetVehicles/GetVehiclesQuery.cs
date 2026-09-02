using Fleet.Application.Features.Vehicles.DTOs;
using MediatR;


namespace Fleet.Application.Features.Vehicles.Queries.GetVehicles
{
    public record GetVehiclesQuery
     : IRequest<IReadOnlyList<VehicleResponse>>;
}
