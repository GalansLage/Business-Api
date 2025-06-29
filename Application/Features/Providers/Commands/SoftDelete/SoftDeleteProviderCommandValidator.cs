
using FluentValidation;

namespace Application.Features.Providers.Commands.SoftDelete
{
    public class SoftDeleteProviderCommandValidator:AbstractValidator<SoftDeleteProviderCommand>
    {
        public SoftDeleteProviderCommandValidator()
        {
            RuleFor(p => p.Id)
               .GreaterThan(0)
               .WithMessage("El Id no puede ser menor que uno");
        }
    }
}
