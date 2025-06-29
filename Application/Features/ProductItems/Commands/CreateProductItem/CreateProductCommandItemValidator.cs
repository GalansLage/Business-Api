using FluentValidation;

namespace Application.Features.ProductItems.Commands.CreateProductItem
{
    public class CreateProductCommandItemValidator:AbstractValidator<CreateProductItemCommand>
    {
        public CreateProductCommandItemValidator()
        {
            RuleFor(p => p.productCode)
                .NotEmpty().WithMessage("El código del producto es obligatorio.")
                .MaximumLength(25).WithMessage("El código del producto no puede exceder los 25 caracteres.")
                .MinimumLength(4).WithMessage("El código del producto no puede ser menor a los 4 caracteres.");

            RuleFor(p => p.productId)
                .GreaterThan(0).WithMessage("El Id del producto no puede ser menor que uno.");

            RuleFor(p => p.itemsAmount)
                .GreaterThan(0).WithMessage("La cantidad de productos no puede ser menor que uno.");
        }
    }
}
