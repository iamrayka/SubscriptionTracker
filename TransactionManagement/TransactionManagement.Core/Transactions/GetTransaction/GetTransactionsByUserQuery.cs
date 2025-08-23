using Shared.Mediator;
using SubscriptionTracker.Shared.Results;
using TransactionManagement.Core.Transactions;

namespace TransactionManagement.Core.Transactions.GetTransaction;

/// <summary>
/// Query to get all transactions for a specific user.
/// </summary>
public class GetTransactionsByUserQuery : IRequest<Result<IEnumerable<Transaction>>>
{
    /// <summary>
    /// ID of the user whose transactions are being fetched.
    /// </summary>
    public Guid UserId { get; init; }
}
