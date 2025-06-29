

using Application.DTOs;
using Domain.Entities.ProductEntity;
using MediatR;

namespace Application.Features.Providers.Commands.CreateProvider
{
    public record CreateProviderCommand(
        string providerName, string providerLastName, string cI, string number
        ) :IRequest<ProviderDto>;
    
}
