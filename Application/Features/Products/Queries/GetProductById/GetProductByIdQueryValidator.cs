

using FluentValidation;

namespace Application.Features.Products.Queries.GetProductById
{
    public class GetProductByIdQueryValidator: AbstractValidator<GetProductByIdQuery>
    {
        public GetProductByIdQueryValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0)
                .WithMessage("El Id no puede ser menor que uno.");
               
        }
    }
}
