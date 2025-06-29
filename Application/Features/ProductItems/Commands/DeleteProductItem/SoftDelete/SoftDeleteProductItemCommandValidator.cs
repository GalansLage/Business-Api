

using FluentValidation;

namespace Application.Features.ProductItems.Commands.DeleteProductItem.SoftDelete
{
    public class SoftDeleteProductItemCommandValidator:AbstractValidator<SoftDeleteProductItemCommand>
    {
        public SoftDeleteProductItemCommandValidator()
        {
            RuleFor(x => x.Id)
               .GreaterThan(0)
               .WithMessage("El Id no puede ser menor que uno.");
        }
    }
}
