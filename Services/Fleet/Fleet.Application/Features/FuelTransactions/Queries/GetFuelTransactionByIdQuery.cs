using Fleet.Application.Features.FuelTransactions.DTOs;
using MediatR;

namespace Fleet.Application.Features.FuelTransactions.Queries
{
    public record GetFuelTransactionByIdQuery(
       Guid Id
   ) : IRequest<FuelTransactionResponse>;
}
