
using FluentValidation;

namespace Application.Features.Products.Commands.DeleteProduct.HardDelete
{
    public class HardDeleteProductValidator:AbstractValidator<HardDeleteProductCommand>
    {
        public HardDeleteProductValidator()
        {
            RuleFor(p => p.Id)
                .GreaterThan(0)
                .WithMessage("El Id no puede ser menor que uno");
        }
    }
}
