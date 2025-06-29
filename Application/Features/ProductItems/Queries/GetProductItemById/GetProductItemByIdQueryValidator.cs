

using FluentValidation;

namespace Application.Features.ProductItems.Queries.GetProductItemById
{
    public class GetProductItemByIdQueryValidator:AbstractValidator<GetProductItemByIdQuery>
    {
        public GetProductItemByIdQueryValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0)
                .WithMessage("El Id no puede ser menor que uno.");
        }
    }
}
