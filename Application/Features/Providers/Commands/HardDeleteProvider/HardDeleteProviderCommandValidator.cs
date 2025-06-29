
using FluentValidation;

namespace Application.Features.Providers.Commands.HardDeleteProvider
{
    public class HardDeleteProviderCommandValidator:AbstractValidator<HardDeleteProviderCommand>
    {
        public HardDeleteProviderCommandValidator()
        {
            RuleFor(p => p.Id)
                .GreaterThan(0)
                .WithMessage("El Id no puede ser menor que uno");
        }
    }
}
