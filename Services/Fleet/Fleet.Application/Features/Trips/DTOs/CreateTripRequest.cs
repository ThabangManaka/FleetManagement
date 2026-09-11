using System;
using System.Collections.Generic;
using System.Text;

namespace Fleet.Application.Features.Trips.DTOs
{
    public record CreateTripRequest(
            Guid VehicleId,
            Guid DriverId,
            string StartLocation,
            string Destination,
            DateTime StartDate,
            int StartMileage,
            string? Notes);
}
