namespace Shared.Mediator;

/// <summary>
/// Defines a handler for a specific request type.
/// </summary>
/// <typeparam name="TRequest">The request type.</typeparam>
/// <typeparam name="TResponse">The response type.</typeparam>
public interface IRequestHandler<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    /// <summary>
    /// Handles the request asynchronously and returns a response.
    /// </summary>
    /// <param name="request">The request to handle.</param>
    /// <returns>A task containing the response.</returns>
    Task<TResponse> HandleAsync(TRequest request);
}