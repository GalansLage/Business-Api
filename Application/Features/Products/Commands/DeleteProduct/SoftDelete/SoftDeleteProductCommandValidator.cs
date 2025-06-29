

using FluentValidation;

namespace Application.Features.Products.Commands.DeleteProduct.SoftDelete
{
    public class SoftDeleteProductCommandValidator:AbstractValidator<SoftDeleteProductCommand>
    {
        public SoftDeleteProductCommandValidator()
        {
            RuleFor(p=>p.Id).GreaterThan(0)
                .WithMessage("El Id no puede ser menor que uno");
        }
    }
}
