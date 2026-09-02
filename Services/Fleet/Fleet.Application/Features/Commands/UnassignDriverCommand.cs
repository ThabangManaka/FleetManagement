using MediatR;


namespace Fleet.Application.Features.Vehicles.Commands.UnassignDriver;

public record UnassignDriverCommand(Guid Id) : IRequest;