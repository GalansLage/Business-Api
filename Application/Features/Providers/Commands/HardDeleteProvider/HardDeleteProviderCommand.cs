
using MediatR;

namespace Application.Features.Providers.Commands.HardDeleteProvider
{
    public record HardDeleteProviderCommand(int Id):IRequest<bool>;
   
}
