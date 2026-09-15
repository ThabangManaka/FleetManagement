using Fleet.Application.Features.FuelTransactions.DTOs;
using MediatR;

namespace Fleet.Application.Features.FuelTransactions.Queries
{
    public record GetFuelTransactionsQuery
       : IRequest<List<FuelTransactionResponse>>;
}
