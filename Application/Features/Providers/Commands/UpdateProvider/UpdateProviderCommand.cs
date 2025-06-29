

using MediatR;

namespace Application.Features.Providers.Commands.UpdateProvider
{
    public record UpdateProviderCommand(
            int Id, string providerName, string providerLastName, string cI, string number
        ):IRequest<bool>;
}
