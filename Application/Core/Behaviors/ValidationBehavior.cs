using FluentValidation;
using MediatR;
using ValidationException = Application.Core.Exceptions.ValidationException;

namespace Application.Core.Behaviors
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (!_validators.Any())
            {
                // Si no hay validadores para este request, continuar al siguiente paso (el handler)
                return await next();
            }

            var context = new ValidationContext<TRequest>(request);

            // Ejecutar todos los validadores en paralelo
            var validationResults = await Task.WhenAll(
                _validators.Select(v => v.ValidateAsync(context, cancellationToken)));

            // Recolectar todos los errores
            var failures = validationResults
                .SelectMany(r => r.Errors)
                .Where(f => f != null)
                .ToList();

            if (failures.Any())
            {
                // Si hay fallos de validación, lanzar una excepción personalizada
                // que contenga todos los errores.
                throw new ValidationException(failures);
            }

            // Si la validación es exitosa, continuar al handler.
            return await next();
        }
    }
}
