

using FluentValidation;

namespace Application.Features.Products.Commands.CreateProduct.CreateProductWithProvider
{
    public class CreateProductWithProviderCommandValidator: AbstractValidator<CreateProductWithProviderCommand>
    {
        public CreateProductWithProviderCommandValidator()
        {
            // --- Reglas para el Producto ---

            RuleFor(p => p.productName)
                .NotEmpty().WithMessage("El nombre del producto es obligatorio.")
                .MaximumLength(35).WithMessage("El nombre del producto no puede exceder los 100 caracteres.")
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


            // --- Reglas para el Proveedor ---

            RuleFor(p => p.providerName)
                .NotEmpty().WithMessage("El nombre del proveedor es obligatorio.")
                .MaximumLength(10).WithMessage("El nombre del proveedor no puede exceder los 10 caracteres.")
                .MinimumLength(3).WithMessage("El nombre del proveedor no puede tener menos de 3 caracteres.");

            RuleFor(p => p.providerLastName)
                .NotEmpty().WithMessage("El apellido del proveedor es obligatorio.")
                .MaximumLength(10).WithMessage("El apellido del proveedor no puede exceder los 10 caracteres.")
                .MinimumLength(3).WithMessage("El apellido del proveedor no puede tener menos de 3 caracteres.");

            RuleFor(p => p.cI) // Cédula de Identidad
                .NotEmpty().WithMessage("La cédula de identidad es obligatoria.")
                .Length(11).WithMessage("La cédula de identidad tiene que tener 11 caracteres.");
            // Puedes añadir una expresión regular si la CI tiene un formato específico
            // .Matches("^[0-9]{7,8}$").WithMessage("Formato de cédula de identidad inválido.");

            RuleFor(p => p.number) // Número de teléfono/contacto
                .NotEmpty().WithMessage("El número de contacto es obligatorio.")
                .MaximumLength(10).WithMessage("El número de contacto no puede exceder los 10 caracteres.")
                .MinimumLength(8).WithMessage("El número de contacto no puede ser menor a los 8 caracteres.");
        }
    }
}
