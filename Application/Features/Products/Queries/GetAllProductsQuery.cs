

using Application.Core;
using Application.DTOs;
using MediatR;

namespace Application.Features.Products.Queries
{
    public record class GetAllProductsQuery(
        int PageNumber = 1,
        int PageSize = 10
        ): IRequest<PagedResponse<ProductWithStockDto>>;
    
}
