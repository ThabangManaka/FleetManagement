using Fleet.Application.Features.Drivers.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fleet.Application.Features.Vehicles.Queries.GetDrivers
{
    public record GetDriversQuery : IRequest<List<DriverResponse>>;
}
