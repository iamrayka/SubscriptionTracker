// ------------------------------------------------------------------------------
//  Project: SubscriptionTracker.Domain
//  File: User.cs
//  Created: 25/04/2025 by Rayka
//  Description: Defines the User entity representing a system user who owns transactions.
// ------------------------------------------------------------------------------
using SubscriptionTracker.Domain.Transactions;

namespace SubscriptionTracker.Domain.Users
{
    /// <summary>
    /// Represents a user of the subscription and transaction tracking system.
    /// </summary>
    public class User
    {
        /// <summary>
        /// Unique identifier for the user.
        /// </summary>
        public Guid Id { get; private set; }

        /// <summary>
        /// User's display name.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// User's email address.
        /// </summary>
        public string Email { get; private set; }

        /// <summary>
        /// Transactions associated with the user.
        /// </summary>
        public List<Transaction> listTransactions { get; private set; }

        /// <summary>
        /// Private parameterless constructor for ORM or serialization purposes.
        /// </summary>
        private User() { }

        /// <summary>
        /// Creates a new User with the specified name and email.
        /// </summary>
        /// <param name="sName">Display name of the user.</param>
        /// <param name="sEmail">Email address of the user.</param>
        public User(string sName, string sEmail)
        {
            if (string.IsNullOrWhiteSpace(sName))
            {
                throw new ArgumentException("User name cannot be empty.", nameof(sName));
            }

            if (string.IsNullOrWhiteSpace(sEmail))
            {
                throw new ArgumentException("User email cannot be empty.", nameof(sEmail));
            }

            Id = Guid.NewGuid();
            this.Name = sName.Trim();
            this.Email = sEmail.Trim().ToLowerInvariant();
            this.listTransactions = new List<Transaction>();
        }

        /// <summary>
        /// Adds a new transaction to the user's transaction list.
        /// </summary>
        /// <param name="transaction">Transaction to add.</param>
        public void AddTransaction(Transaction transaction)
        {
            if (transaction == null)
            {
                throw new ArgumentNullException(nameof(transaction));
            }

            listTransactions.Add(transaction);
        }
    }
}
