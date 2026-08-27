using MediatR;

namespace Fleet.Application.Features.Commands
{
    public record DeleteDriverCommand(
        Guid Id
    ) : IRequest;
}