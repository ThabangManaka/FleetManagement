using Fleet.Application.Features.Drivers.DTOs;
using MediatR;


namespace Fleet.Application.Features.Commands
{
    public record CreateDriverCommand(
        CreateDriverRequest Request
    ) : IRequest<Guid>;

}