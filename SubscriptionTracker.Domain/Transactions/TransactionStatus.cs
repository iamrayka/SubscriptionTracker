// ------------------------------------------------------------------------------
//  Project: SubscriptionTracker.Domain
//  File: TransactionStatus.cs
//  Created: 25/04/2025 by Rayka
//  Description: Defines the status of a transaction (cleared, scheduled, pending).
// ------------------------------------------------------------------------------
namespace SubscriptionTracker.Domain.Transactions
{
    /// <summary>
    /// Represents the current status of the transaction.
    /// </summary>
    public enum TransactionStatus
    {
        Cleared,    // The transaction has been confirmed and completed
        Scheduled,  // The transaction is scheduled for a future date
        Pending     // The transaction is pending user or system confirmation
    }
}
