
using FluentValidation;

namespace Application.Features.Products.Queries
{
    public class GetAllProductsQueryValidator : AbstractValidator<GetAllProductsQuery>
    {
        public GetAllProductsQueryValidator()
        {
            RuleFor(x => x.PageNumber).GreaterThan(0).WithMessage("El número de página debe ser mayor que 0.");

            RuleFor(x => x.PageSize)
                .GreaterThan(0)
                .WithMessage("El tamaño de página debe ser mayor que 0.")
                .LessThanOrEqualTo(100)
                .WithMessage("El tamaño de página no puede ser mayor que 100.");
        }

    }
}
