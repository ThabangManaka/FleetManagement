using Fleet.Application.Features.Drivers.DTOs;
using MediatR;


namespace Fleet.Application.Features.Commands
{
    public record UpdateDriverCommand(
    Guid Id,
    UpdateDriverRequest Request
) : IRequest;
}