using Shared.Mediator;
using SubscriptionTracker.Shared.Results;

namespace TransactionManagement.Core.Transactions.DeleteTransaction;

/// <summary>
/// Command to delete a transaction by its ID.
/// </summary>
public class DeleteTransactionCommand : IRequest<Result>
{
    /// <summary>
    /// The unique identifier of the transaction to be deleted.
    /// </summary>
    public Guid Id { get; init; }
}
