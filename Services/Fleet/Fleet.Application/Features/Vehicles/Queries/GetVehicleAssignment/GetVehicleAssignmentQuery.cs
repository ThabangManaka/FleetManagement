using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fleet.Application.Features.Vehicles.Queries.GetVehicleAssignment
{
    public record GetVehicleAssignmentQuery(
        Guid Id
    ) : IRequest<VehicleAssignmentDto?>;
}
