

using FluentValidation;

namespace Application.Features.Transactions.Commands.StartTransaction
{
    public class StartTransactionCommandValidator:AbstractValidator<StartTransactionCommand>
    {
        public StartTransactionCommandValidator()
        {
            RuleFor(cmd => cmd.products)
                .NotEmpty()
                .WithMessage("La transacción debe contener al menos un ítem.");
            RuleForEach(cmd => cmd.products)
                .SetValidator(new TransactionItemDataValidator());
        }
    }
}
