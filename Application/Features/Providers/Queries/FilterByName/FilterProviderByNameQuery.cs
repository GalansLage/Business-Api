
using Application.Core;
using Application.DTOs;
using MediatR;

namespace Application.Features.Providers.Queries.FilterByName
{
    public record FilterProviderByNameQuery(
        int pageNumber = 1,
        int pageSize = 10,
        string searchName = ""
        ):IRequest<PagedResponse<ProviderDto>>;
}
