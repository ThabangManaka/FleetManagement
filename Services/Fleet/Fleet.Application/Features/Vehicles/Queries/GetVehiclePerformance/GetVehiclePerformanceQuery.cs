using Fleet.Application.Features.Vehicles.DTOs;
using MediatR;


namespace Fleet.Application.Features.Vehicles.Queries.GetVehiclePerformance
{
    public record GetVehiclePerformanceQuery(
       Guid VehicleId
   ) : IRequest<VehiclePerformanceResponse>;
}
