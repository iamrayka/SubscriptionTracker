namespace SubscriptionTracker.Shared.Results;

/// <summary>
/// Represents the result of an operation that returns a value of type T.
/// </summary>
/// <typeparam name="T">The type of the value returned on success.</typeparam>
public class Result<T> : Result
{
    /// <summary>
    /// The value returned by the operation (if successful).
    /// Will be null if the operation failed.
    /// </summary>
    public T? Value { get; }

    /// <summary>
    /// Private constructor used by the static factory methods (Success/Failure).
    /// </summary>
    private Result(bool isSuccess, T? value, string? error)
        : base(isSuccess, error)
    {
        Value = value;
    }

    /// <summary>
    /// Creates a successful result with a value.
    /// </summary>
    /// <param name="value">The value returned by the operation.</param>
    /// <returns>A successful Result containing the given value.</returns>
    public static Result<T> Success(T value)
    {
        if (value == null)
            throw new ArgumentNullException(nameof(value), "Success value cannot be null.");

        return new Result<T>(true, value, null);
    }

    /// <summary>
    /// Creates a failed result with an error message.
    /// </summary>
    /// <param name="error">The error message.</param>
    /// <returns>A failed Result with the provided error message.</returns>
    public static new Result<T> Failure(string error)
    {
        if (string.IsNullOrWhiteSpace(error))
            throw new ArgumentException("Error message must be provided for a failure result.", nameof(error));

        return new Result<T>(false, default, error);
    }

    /// <summary>
    /// Returns a string representation for debugging/logging purposes.
    /// </summary>
    public override string ToString() =>
        IsSuccess ? $"Success: {Value}" : $"Failure: {Error}";
}