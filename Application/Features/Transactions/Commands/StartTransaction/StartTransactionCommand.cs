
using Domain.Entities.ProductItemEntity;
using MediatR;

namespace Application.Features.Transactions.Commands.StartTransaction
{
    public record TransactionItemData(int ProductId, int Quantity);
    public record StartTransactionCommand(List<TransactionItemData> products):IRequest<bool>;
    
}
