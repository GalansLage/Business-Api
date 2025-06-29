
using Application.Features.Transactions.Commands.StartTransaction;
using MediatR;

namespace Application.Features.Transactions.Commands.AddNewProducts
{
    public record AddNewProductsCommand(List<TransactionItemData> products):IRequest<bool>;

}
