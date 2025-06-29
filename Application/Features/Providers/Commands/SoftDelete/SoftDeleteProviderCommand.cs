

using MediatR;

namespace Application.Features.Providers.Commands.SoftDelete
{
    public record SoftDeleteProviderCommand(int Id):IRequest<bool>;
   
}
