
using MediatR;

namespace Application.Features.Transactions.Commands.ConfirmTransaction
{
    public record ConfirmTransactionCommand():IRequest<decimal>;
    
    
}
