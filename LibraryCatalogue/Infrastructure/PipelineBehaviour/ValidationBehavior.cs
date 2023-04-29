using FluentValidation;
using MediatR;

namespace LibraryCatalogue.Infrastructure.PipelineBehaviour;

public record ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    private readonly IValidator<TRequest> _validator;

    public ValidationBehavior(IValidator<TRequest> validator) => _validator = validator;

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken ct)
    {
        var validationResult = await _validator.ValidateAsync(request, ct);

        //Todo: Return a nice response to the consumer.
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        var response = await next();
        return response;
    }
}