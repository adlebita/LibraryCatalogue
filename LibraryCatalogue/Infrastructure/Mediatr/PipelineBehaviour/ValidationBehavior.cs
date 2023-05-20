using FluentValidation;
using MediatR;

namespace LibraryCatalogue.Infrastructure.Mediatr.PipelineBehaviour;

public record ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    private readonly IValidator<TRequest> _validator;

    // Todo: YOU MUST Rewrite this because an exception is thrown if a TRequest does not have a corresponding validator.
    // The expectation is, if there is no validation matching TRequest then go straight through to next delegate.
    // You have tried setting this to nullable does not work--- Idk why.
    // Probably because the service provider looks for a corresponding IValidator and blows up when it can't find anything.
    // I mean, think about it, how can you DI a NULLABLE object... idiot.
    // I think the best approach may be to get scan or get list and iterate through each validation.
    // 20/05/2023 @ 19:27
    public ValidationBehavior(IValidator<TRequest> validator) => _validator = validator;

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken ct)
    {
        var validationResult = await _validator.ValidateAsync(request, ct);

        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        var response = await next();
        return response;
    }
}