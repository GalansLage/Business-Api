using MediatR;

namespace Application.Features.ProductItems.Commands.CreateProductItem
{
    public record CreateProductItemCommand(
        string productCode, int productId,int itemsAmount
        ) :IRequest<string>;

}
