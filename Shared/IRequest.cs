namespace Shared.Mediator;

/// <summary>
/// Represents a request with a return type.
/// Used with <see cref="IMediator"/> to indicate the expected response type.
/// </summary>
/// <typeparam name="TResponse">The type of the response returned by the handler.</typeparam>
public interface IRequest<TResponse> { }