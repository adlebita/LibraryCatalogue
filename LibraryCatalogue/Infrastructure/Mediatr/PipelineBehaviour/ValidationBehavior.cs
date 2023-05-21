using FluentValidation;
using MediatR;

namespace LibraryCatalogue.Infrastructure.Mediatr.PipelineBehaviour;

public record ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;
    
    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators) => _validators = validators;

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken ct)
    {
        foreach (var validator in _validators)
        {
            var validationResult = await validator.ValidateAsync(request, ct);
            
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }
        }

        var response = await next();
        return response;
    }
}