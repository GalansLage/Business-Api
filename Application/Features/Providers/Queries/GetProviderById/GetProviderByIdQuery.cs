

using Application.DTOs;
using MediatR;

namespace Application.Features.Providers.Queries.GetProviderById
{
    public record GetProviderByIdQuery(int Id):IRequest<ProviderDto>;
}
