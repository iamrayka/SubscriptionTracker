using Shared.Mediator;
using SubscriptionTracker.Shared.Results;

namespace TransactionManagement.Core.Transactions.CreateTransaction;

/// <summary>
/// Command to create a new transaction.
/// </summary>
public class CreateTransactionCommand : IRequest<Result<Transaction>>
{
    /// <summary>
    /// The ID of the user associated with the transaction.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// The date the transaction took place.
    /// </summary>
    public DateTime TransactionDate { get; set; }

    /// <summary>
    /// Description or label for the transaction.
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// The monetary amount of the transaction.
    /// </summary>
    public decimal Amount { get; set; }
}
