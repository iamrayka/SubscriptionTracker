using TransactionManagement.Core.Transactions;

namespace TransactionManagement.Infrastructure.Transactions;

/// <summary>
/// In-memory implementation of ITransactionRepository for testing and prototyping.
/// </summary>
public class TransactionRepository : ITransactionRepository
{
    private static readonly List<Transaction> _transactions = new();

    /// <summary>
    /// Adds a new transaction to the in-memory store.
    /// </summary>
    public Task CreateAsync(Transaction transaction)
    {
        _transactions.Add(transaction);
        return Task.CompletedTask;
    }

    /// <summary>
    /// Returns all transactions associated with a given user.
    /// </summary>
    public Task<IEnumerable<Transaction>> GetByUserIdAsync(Guid userId)
    {
        var result = _transactions.Where(t => t.UserId == userId);
        return Task.FromResult(result);
    }

    /// <summary>
    /// Finds a transaction by its ID.
    /// </summary>
    public Task<Transaction?> GetByIdAsync(Guid id)
    {
        var transaction = _transactions.FirstOrDefault(t => t.Id == id);
        return Task.FromResult(transaction);
    }

    /// <summary>
    /// Updates an existing transaction (in-memory simulation).
    /// </summary>
    public Task UpdateAsync(Transaction transaction)
    {
        var index = _transactions.FindIndex(t => t.Id == transaction.Id);
        if (index >= 0)
        {
            _transactions[index] = transaction;
        }
        return Task.CompletedTask;
    }

    /// <summary>
    /// Deletes a transaction by ID (if it exists).
    /// </summary>
    public Task DeleteAsync(Guid id)
    {
        var transaction = _transactions.FirstOrDefault(t => t.Id == id);
        if (transaction != null)
        {
            _transactions.Remove(transaction);
        }
        return Task.CompletedTask;
    }
}