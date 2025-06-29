using Application.Core;
using Application.DTOs;
using MediatR;

namespace Application.Features.ProductItems.Queries.GetAllProductsItems
{
    public record GetAllProductItemQuery(
            int PageNumber = 1,
            int PageSize = 10
        ):IRequest<PagedResponse<ProductItemDto>>;
    
    
}
