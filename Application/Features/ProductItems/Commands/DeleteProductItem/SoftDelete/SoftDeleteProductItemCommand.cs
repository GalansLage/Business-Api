

using MediatR;

namespace Application.Features.ProductItems.Commands.DeleteProductItem.SoftDelete
{
    public record SoftDeleteProductItemCommand(int Id):IRequest<bool>;
    
   
}
