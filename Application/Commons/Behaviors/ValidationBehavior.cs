using Application.Commons.Interfaces;
using Application.Commons.Errors;
using Application.Commons.Exceptions;
using FluentValidation;
using MediatR;

namespace Application.Commons.Behaviors;

public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IBaseCommand
{
    //validadores de fluentValidation
    private readonly IEnumerable<IValidator<TRequest>>? _validators;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>>? validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (!_validators!.Any())
        {
            return await next();
        }

        //creando un contexto para las validaciones
        var context = new ValidationContext<TRequest>(request);

        //obteniendo los errores de las validaciones
        var validationErrors = _validators!
        .Select(validators => validators.Validate(context))
        .Where(validationResult => validationResult.Errors.Any())
        .SelectMany(validationResult => validationResult.Errors)
        .Select(validationFailure => new ValidationError(
            validationFailure.PropertyName,
            validationFailure.ErrorMessage
        )).ToList();

        if (validationErrors.Any())
        {
            throw new DataValidationException(validationErrors);
        }

        return await next();

    }
}
