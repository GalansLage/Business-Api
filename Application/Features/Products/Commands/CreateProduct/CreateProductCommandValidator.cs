
using FluentValidation;

namespace Application.Features.Products.Commands.CreateProduct
{
    public class CreateProductCommandValidator:AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            // --- Reglas para el Producto ---

            RuleFor(p => p.productName)
                .NotEmpty().WithMessage("El nombre del producto es obligatorio.")
                .MaximumLength(35).WithMessage("El nombre del producto no puede exceder los 35 caracteres.")
                .MinimumLength(4).WithMessage("El nombre del producto no puede ser menor a los 4 caracteres.");


            RuleFor(p => p.amountPrice)
                .GreaterThan(0).WithMessage("El precio debe ser mayor que cero.");

            RuleFor(p => p.amountCost)
                .GreaterThanOrEqualTo(0).WithMessage("El costo no puede ser negativo.")
                // Regla personalizada para asegurar que el costo no sea mayor que el precio
                .LessThan(p => p.amountPrice).WithMessage("El costo no puede ser mayor o igual al precio de venta.");

            RuleFor(p => p.currencyEnum)
                // IsInEnum() valida que el valor recibido sea un miembro válido del enum.
                .IsInEnum().WithMessage("El tipo de moneda proporcionado no es válido.");


            RuleFor(p => p.category)
                .NotEmpty().WithMessage("La categoría es obligatoria.")
                .MaximumLength(50).WithMessage("La categoría no puede exceder los 50 caracteres.");
           
        }
    }
}
