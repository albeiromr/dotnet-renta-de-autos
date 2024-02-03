using Application.Commons.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Commons.Behaviors;

public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IBaseCommand
{
    private readonly ILogger<TRequest>? _logger;
    public LoggingBehavior(ILogger<TRequest>? logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var name = request.GetType().Name;

        try
        {
            _logger!.LogInformation($"Executing the command {name}");
            var result = await next();
            _logger!.LogInformation($"The command {name} was executed successfully");
            return result;
        }
        catch
        {
            _logger!.LogError($"The command {name} had errors");
            throw;
        }
    }
}
