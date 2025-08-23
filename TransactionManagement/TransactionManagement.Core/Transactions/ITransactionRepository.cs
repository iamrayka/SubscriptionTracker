namespace TransactionManagement.Core.Transactions;

/// <summary>
/// Defines contract for working with transaction data (CRUD).
/// </summary>
public interface ITransactionRepository
{
    /// <summary>
    /// Creates a new transaction.
    /// </summary>
    Task CreateAsync(Transaction transaction);

    /// <summary>
    /// Retrieves all transactions belonging to a specific user.
    /// </summary>
    Task<IEnumerable<Transaction>> GetByUserIdAsync(Guid userId);

    /// <summary>
    /// Retrieves a transaction by its unique ID.
    /// </summary>
    Task<Transaction?> GetByIdAsync(Guid id);

    /// <summary>
    /// Updates an existing transaction.
    /// </summary>
    Task UpdateAsync(Transaction transaction);

    /// <summary>
    /// Deletes a transaction by ID.
    /// </summary>
    Task DeleteAsync(Guid id);
}