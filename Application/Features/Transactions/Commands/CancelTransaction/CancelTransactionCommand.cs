

using MediatR;

namespace Application.Features.Transactions.Commands.CancelTransaction
{
    public record CancelTransactionCommand():IRequest<bool>;

}
