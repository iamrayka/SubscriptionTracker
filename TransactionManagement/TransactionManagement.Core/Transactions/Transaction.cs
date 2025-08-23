// ------------------------------------------------------------------------------
//  Project: TransactionManagement.Core
//  File: Transaction.cs
//  Created: 04/06/2025 by Rayka
//  Description: Defines the Transaction entity representing a basic financial record
//  containing amount, date, and description for a specific user.
// ------------------------------------------------------------------------------

namespace TransactionManagement.Core.Transactions;

/// <summary>
/// Represents a basic financial transaction associated with a user.
/// </summary>
public class Transaction
{
    /// <summary>
    /// Unique identifier for the transaction.
    /// </summary>
    public Guid Id { get; private set; }

    /// <summary>
    /// Identifier of the user who owns this transaction.
    /// </summary>
    public Guid UserId { get; private set; }

    /// <summary>
    /// The date the transaction took place (e.g., purchase or expense date).
    /// </summary>
    public DateTime TransactionDate { get; private set; }

    /// <summary>
    /// Short description or label for the transaction (e.g., "Tesco groceries").
    /// </summary>
    public string Description { get; private set; }

    /// <summary>
    /// The monetary amount of the transaction (positive for income, negative for expense).
    /// </summary>
    public decimal Amount { get; private set; }

    /// <summary>
    /// Creates a new transaction with the specified user ID, date, description, and amount.
    /// </summary>
    public Transaction(Guid userId, DateTime transactionDate, string description, decimal amount)
    {
        if (userId == Guid.Empty)
            throw new ArgumentException("User ID cannot be empty.", nameof(userId));

        if (string.IsNullOrWhiteSpace(description))
            throw new ArgumentException("Transaction description cannot be empty.", nameof(description));

        Id = Guid.NewGuid();
        UserId = userId;
        TransactionDate = transactionDate;
        Description = description.Trim();
        Amount = amount;
    }

    /// <summary>
    /// Updates the transaction's core fields.
    /// </summary>
    public void Update(DateTime transactionDate, string description, decimal amount)
    {
        if (string.IsNullOrWhiteSpace(description))
            throw new ArgumentException("Transaction description cannot be empty.", nameof(description));

        TransactionDate = transactionDate;
        Description = description.Trim();
        Amount = amount;
    }
}