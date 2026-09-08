using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fleet.Application.Features.Commands
{
    public record DeleteMaintenanceCommand(
       Guid Id
   ) : IRequest;
}
