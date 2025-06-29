using Application.DTOs;
using MediatR;

namespace Application.Features.ProductItems.Queries.GetProductItemById
{
    public record GetProductItemByIdQuery(int Id):IRequest<ProductItemDto>;
    
}
