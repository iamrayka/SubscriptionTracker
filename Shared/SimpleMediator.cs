using Microsoft.Extensions.DependencyInjection;

namespace Shared.Mediator;

/// <summary>
/// Simple implementation of the mediator pattern for decoupled request handling.
/// </summary>
public class SimpleMediator : IMediator
{
    private readonly IServiceProvider _provider;

    public SimpleMediator(IServiceProvider provider)
    {
        _provider = provider;
    }

    public async Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request)
    {
        // Dynamically resolve the correct handler type based on the request and response types
        var handlerType = typeof(IRequestHandler<,>).MakeGenericType(request.GetType(), typeof(TResponse));

        // Resolve the handler instance from DI container
        var handler = _provider.GetRequiredService(handlerType);

        // Dynamically invoke the handler method with the request
        return await ((dynamic)handler).HandleAsync((dynamic)request);
    }
}