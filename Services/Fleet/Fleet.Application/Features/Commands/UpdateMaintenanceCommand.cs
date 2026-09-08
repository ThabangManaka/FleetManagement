using MediatR;

using MediatR;

namespace Fleet.Application.Features.Commands
{
      public record UpdateMaintenanceCommand(
        Guid Id,
        UpdateMaintenanceRequest Request
    ) : IRequest;
}
