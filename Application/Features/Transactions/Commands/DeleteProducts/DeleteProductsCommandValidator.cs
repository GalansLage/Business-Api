
using Application.Features.Transactions.Commands.StartTransaction;
using FluentValidation;

namespace Application.Features.Transactions.Commands.DeleteProducts
{
    public class DeleteProductsCommandValidator:AbstractValidator<DeleteProductsCommand>
    {
        public DeleteProductsCommandValidator()
        {
            RuleFor(cmd => cmd.ItemsToRemove)
            .NotEmpty().WithMessage("Se debe especificar al menos un ítem para eliminar.");

            RuleForEach(cmd => cmd.ItemsToRemove).ChildRules(item =>
            {
                item.RuleFor(i => i.ProductItemId)
                    .GreaterThan(0).WithMessage("El ID del ítem de producto debe ser válido.");
            });
        }
    }
}
