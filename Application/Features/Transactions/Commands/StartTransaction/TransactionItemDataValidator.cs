

using FluentValidation;

namespace Application.Features.Transactions.Commands.StartTransaction
{
    public class TransactionItemDataValidator:AbstractValidator<TransactionItemData>
    {
        public TransactionItemDataValidator()
        {
            RuleFor(item => item.ProductId)
                .GreaterThan(0)
                .WithMessage("El ID del producto en cada ítem debe ser válido.");

            RuleFor(item => item.Quantity)
                .GreaterThan(0)
                .WithMessage("La cantidad en cada ítem debe ser mayor que cero.");
        }
    }
}
