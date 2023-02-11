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

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        //Request
        var ServiceName = typeof(TRequest).GetTypeInfo().FullName.Split(".Actions.")[0];
        var FeatrueName = typeof(TRequest).GetTypeInfo().FullName.Split(".Actions.")[1];

        _logger.LogInformation($"| {ServiceName} | {FeatrueName} | Request | {JsonConvert.SerializeObject(request)}");
        
        try
        {
            return await next();
            
        }
        catch (Exception ex)
        {
            _logger.LogError($"| {ServiceName} | {FeatrueName} | {ex.GetType().FullName.Split('.')[^1]} | {ex.Message}");
            throw ex;
        }
    }
}