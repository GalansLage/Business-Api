

using MediatR;

namespace Application.Features.Products.Commands.DeleteProduct.SoftDelete
{
    public record SoftDeleteProductCommand(int Id):IRequest<bool>;
        
}
