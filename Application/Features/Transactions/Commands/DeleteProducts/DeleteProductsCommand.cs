

using Application.Features.Transactions.Commands.StartTransaction;
using MediatR;

namespace Application.Features.Transactions.Commands.DeleteProducts
{
    public record TransactionItemToRemove(
    int ProductItemId
);
    public record DeleteProductsCommand(List<TransactionItemToRemove> ItemsToRemove) :IRequest<bool>;
    
}
