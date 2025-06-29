using Application.Features.Transactions.Commands.StartTransaction;
using FluentValidation;

namespace Application.Features.Transactions.Commands.AddNewProducts
{
    public class AddNewProductsCommandValidator:AbstractValidator<AddNewProductsCommand>
    {
        public AddNewProductsCommandValidator()
        {
            RuleFor(cmd => cmd.products)
                .NotEmpty()
                .WithMessage("La transacción debe contener al menos un ítem.");
            RuleForEach(cmd => cmd.products)
                .SetValidator(new TransactionItemDataValidator());
        }
    }
}
