using System.Reflection;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Shared.Behaviours;

public class LoggingBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : MediatR.IRequest<TResponse> where TResponse : new ()
{
    private readonly ILogger<LoggingBehaviour<TRequest, TResponse>> _logger;

    public LoggingBehaviour(ILogger<LoggingBehaviour<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        //Request
        string ServiceName = String.Empty;
        string FeatrueName = String.Empty;
        if (typeof(TRequest).GetTypeInfo().FullName.Contains(".Actions."))
        {
            ServiceName = typeof(TRequest).GetTypeInfo().FullName.Split(".Actions.")[0];
            FeatrueName = typeof(TRequest).GetTypeInfo().FullName.Split(".Actions.")[1];
        }
        else if (typeof(TRequest).GetTypeInfo().FullName.Contains(".EventConsumerActions."))
        {
            ServiceName = typeof(TRequest).GetTypeInfo().FullName.Split(".EventConsumerActions.")[0];
            FeatrueName = typeof(TRequest).GetTypeInfo().FullName.Split(".EventConsumerActions.")[1];
        }
        
        _logger.LogInformation($"| {ServiceName} | {FeatrueName} | Request | {JsonConvert.SerializeObject(request)}");
        
        try
        {
            return await next();

        }
        catch (Exception ex)
        {
            _logger.LogError($"| {ServiceName} | {FeatrueName} | {ex.GetType().Name.Split('.')[^1]} | {ex.Message}");
            throw ex;
        }
    }
}