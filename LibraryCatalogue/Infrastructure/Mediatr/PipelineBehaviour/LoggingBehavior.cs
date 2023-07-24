using MediatR;

namespace LibraryCatalogue.Infrastructure.Mediatr.PipelineBehaviour;

public record LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;

    public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger) => _logger = logger;

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken ct)
    {
        _logger.LogInformation("Handling request: {RequestType}", typeof(TRequest).FullName);
        var response = await next();
        
        _logger.LogInformation("Handled request: {RequestType} with {ResponseValue}", typeof(TRequest).FullName, response);
        return response;
    }
}