// ------------------------------------------------------------------------------
//  Project: SubscriptionTracker.Domain
//  File: Transaction.cs
//  Created: 25/04/2025 by Rayka
//  Description: Defines the Transaction entity representing a single financial event.
// ------------------------------------------------------------------------------
using SubscriptionTracker.Domain.Common;

namespace SubscriptionTracker.Domain.Transactions
{
    /// <summary>
    /// Represents a single financial transaction recorded by the system.
    /// </summary>
    public class Transaction
    {
        /// <summary>
        /// Unique identifier for the transaction.
        /// </summary>
        public Guid Id { get; private set; }

        /// <summary>
        /// The date when the transaction occurred.
        /// </summary>
        public DateTime Date { get; private set; }

        /// <summary>
        /// The timestamp when the transaction was inserted into the system.
        /// </summary>
        public DateTime InsertedAt { get; private set; }

        /// <summary>
        /// Raw description from the bank feed or manual entry.
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// Cleaned or parsed vendor name.
        /// </summary>
        public string Vendor { get; private set; }

        /// <summary>
        /// The monetary amount involved in the transaction.
        /// </summary>
        public Money Amount { get; private set; }

        /// <summary>
        /// Category classification of the transaction.
        /// </summary>
        public Category Category { get; private set; }

        /// <summary>
        /// Tags associated with the transaction for flexible labeling.
        /// </summary>
        public List<Tag> Tags { get; private set; }

        /// <summary>
        /// Indicates where the transaction originated from.
        /// </summary>
        public TransactionSource Source { get; private set; }

        /// <summary>
        /// The current status of the transaction (Cleared, Pending, Scheduled).
        /// </summary>
        public TransactionStatus Status { get; private set; }

        /// <summary>
        /// Private parameterless constructor for ORM or serialization purposes.
        /// </summary>
        private Transaction() { }
    }
}
