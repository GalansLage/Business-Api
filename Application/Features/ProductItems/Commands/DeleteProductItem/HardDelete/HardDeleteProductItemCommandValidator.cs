

using FluentValidation;

namespace Application.Features.ProductItems.Commands.DeleteProductItem.HardDelete
{
    public class HardDeleteProductItemCommandValidator:AbstractValidator<HardDeleteProductItemCommand>
    {
        public HardDeleteProductItemCommandValidator()
        {
            RuleFor(x => x.Id)
               .GreaterThan(0)
               .WithMessage("El Id no puede ser menor que uno.");
        }
    }
}
