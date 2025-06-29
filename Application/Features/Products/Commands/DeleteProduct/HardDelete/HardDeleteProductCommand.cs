

using MediatR;

namespace Application.Features.Products.Commands.DeleteProduct.HardDelete
{
    public record HardDeleteProductCommand(int Id):IRequest<bool>;
    
}
