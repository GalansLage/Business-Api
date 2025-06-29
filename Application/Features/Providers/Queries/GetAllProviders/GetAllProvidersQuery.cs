

using Application.Core;
using Application.DTOs;
using MediatR;

namespace Application.Features.Providers.Queries.GetAllProviders
{
    public record GetAllProvidersQuery(int pageNumber = 1, int pageSize = 10):IRequest<PagedResponse<ProviderDto>>;
}
