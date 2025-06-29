
using FluentValidation;

namespace Application.Features.Providers.Queries.GetProviderById
{
    public class GetProviderByIdQueryValidator:AbstractValidator<GetProviderByIdQuery>
    {
        public GetProviderByIdQueryValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0)
                .WithMessage("El Id no puede ser menor que uno.");
        }
    }
}
