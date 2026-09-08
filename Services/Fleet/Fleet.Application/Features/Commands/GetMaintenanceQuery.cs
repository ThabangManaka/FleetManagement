using MediatR;

namespace Fleet.Application.Features.Commands
{
    public record GetMaintenanceQuery(
    Guid Id
) : IRequest<MaintenanceResponse?>;
}