using Shared.Mediator;
using SubscriptionTracker.Shared.Results;
using TransactionManagement.Core.Transactions;

namespace TransactionManagement.Core.Transactions.GetTransaction;

/// <summary>
/// Handles retrieval of transactions by user ID.
/// </summary>
public class GetTransactionsByUserQueryHandler : IRequestHandler<GetTransactionsByUserQuery, Result<IEnumerable<Transaction>>>
{
    private readonly ITransactionRepository _repository;

    public GetTransactionsByUserQueryHandler(ITransactionRepository repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    /// <summary>
    /// Fetches all transactions for the specified user.
    /// </summary>
    /// <param name="request">The query containing the user's ID.</param>
    /// <returns>A successful result with a list of transactions or a failure with an error.</returns>
    public async Task<Result<IEnumerable<Transaction>>> HandleAsync(GetTransactionsByUserQuery request)
    {
        if (request.UserId == Guid.Empty)
            return Result<IEnumerable<Transaction>>.Failure("User ID cannot be empty.");

        try
        {
            var transactions = await _repository.GetByUserIdAsync(request.UserId);
            return Result<IEnumerable<Transaction>>.Success(transactions);
        }
        catch (Exception ex)
        {
            // Log the exception internally (ILogger or Console in simple cases)
            Console.Error.WriteLine(ex); // Should be replaced with logger later 

            return Result<IEnumerable<Transaction>>.Failure("An error occurred while retrieving transactions.");
        }
    }
}