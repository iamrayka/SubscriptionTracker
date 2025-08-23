using Shared.Mediator;
using SubscriptionTracker.Shared.Results;

namespace TransactionManagement.Core.Transactions.UpdateTransaction;

/// <summary>
/// Command to update an existing transaction.
/// </summary>
public class UpdateTransactionCommand : IRequest<Result<Transaction>>
{
    /// <summary>
    /// ID of the transaction to update.
    /// </summary>
    public Guid Id { get; init; }

    /// <summary>
    /// Updated transaction date.
    /// </summary>
    public DateTime TransactionDate { get; set; }

    /// <summary>
    /// Updated description.
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Updated amount.
    /// </summary>
    public decimal Amount { get; set; }
}