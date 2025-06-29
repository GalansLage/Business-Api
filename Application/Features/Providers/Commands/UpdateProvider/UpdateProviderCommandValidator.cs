

using FluentValidation;

namespace Application.Features.Providers.Commands.UpdateProvider
{
    public class UpdateProviderCommandValidator:AbstractValidator<UpdateProviderCommand>
    {
        public UpdateProviderCommandValidator()
        {
            RuleFor(p => p.Id)
               .GreaterThan(0)
               .WithMessage("El Id no puede ser menor que uno");
            RuleFor(p => p.providerName)
             .NotEmpty().WithMessage("El nombre del provedor es obligatorio.")
             .MaximumLength(35).WithMessage("El nombre del provedor no puede exceder los 35 caracteres.")
             .MinimumLength(4).WithMessage("El nombre del provedor no puede ser menor a los 4 caracteres.");

            RuleFor(p => p.providerLastName)
             .NotEmpty().WithMessage("El apellido del provedor es obligatorio.")
             .MaximumLength(35).WithMessage("El apellido del provedor no puede exceder los 35 caracteres.")
             .MinimumLength(4).WithMessage("El apellido del provedor no puede ser menor a los 4 caracteres.");

            RuleFor(p => p.cI)
                .NotEmpty().WithMessage("El canet de identidad es obligatorio.")
                .Length(11).WithMessage("El canet de identidad tiene que tener once caracteres");

            RuleFor(p => p.number) // Número de teléfono/contacto
              .NotEmpty().WithMessage("El número de contacto es obligatorio.")
              .MaximumLength(10).WithMessage("El número de contacto no puede exceder los 10 caracteres.")
              .MinimumLength(8).WithMessage("El número de contacto no puede ser menor a los 8 caracteres.");
        }
    }
}
