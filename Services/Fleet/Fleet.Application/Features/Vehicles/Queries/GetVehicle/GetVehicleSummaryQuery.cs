using Fleet.Application.Features.FuelTransactions.DTOs;
using MediatR;


namespace Fleet.Application.Features.Vehicles.Queries.GetVehicle
{
    public record GetVehicleSummaryQuery(
     Guid VehicleId
 ) : IRequest<VehicleSummaryResponse>;
}
