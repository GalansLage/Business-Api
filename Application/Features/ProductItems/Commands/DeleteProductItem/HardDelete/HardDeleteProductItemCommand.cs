using MediatR;

namespace Application.Features.ProductItems.Commands.DeleteProductItem.HardDelete
{
    public record HardDeleteProductItemCommand(int Id):IRequest<bool>;
    
   
}
