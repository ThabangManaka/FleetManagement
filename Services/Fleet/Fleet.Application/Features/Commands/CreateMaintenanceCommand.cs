using Fleet.Application.Features.Maintenance.DTOs;
using MediatR;


namespace Fleet.Application.Features.Commands
{
    public record CreateMaintenanceCommand(
      CreateMaintenanceRequest Request
  ) : IRequest<Guid>;
}

