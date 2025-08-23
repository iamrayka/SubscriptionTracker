namespace Shared.Mediator;

/// <summary>
/// Defines a contract for sending requests to their corresponding handlers.
/// </summary>
public interface IMediator
{
    /// <summary>
    /// Sends a request to the appropriate handler and returns the response.
    /// </summary>
    /// <typeparam name="TResponse">The type of the response.</typeparam>
    /// <param name="request">The request object implementing <see cref="IRequest{TResponse}"/>.</param>
    /// <returns>The result of handling the request.</returns>
    Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request);
}