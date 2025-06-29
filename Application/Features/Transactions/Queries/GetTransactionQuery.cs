
using Application.DTOs;
using MediatR;

namespace Application.Features.Transactions.Queries
{
    public record GetTransactionQuery():IRequest<TransactionDto>;
    
    
}
