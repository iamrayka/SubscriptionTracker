using TransactionManagement.Core.Transactions;

namespace TransactionManagement.Infrastructure.Transactions
{
    public class TransactionRepository : ITransactionRepository
    {
        private static readonly List<Transaction> _transactions = new();

        public Task CreateAsync(Transaction transaction)
        {
            _transactions.Add(transaction);
            return Task.CompletedTask;
        }

        public Task<List<Transaction>> GetByUserIdAsync(Guid userId)
        {
            return Task.FromResult(_transactions.Where(t => t.UserId == userId).ToList());
        }

        public Task<Transaction?> GetByIdAsync(Guid id)
        {
            return Task.FromResult(_transactions.FirstOrDefault(t => t.Id == id));
        }

        public Task UpdateAsync(Transaction transaction) => Task.CompletedTask;

        public Task DeleteAsync(Guid id)
        {
            var tx = _transactions.FirstOrDefault(x => x.Id == id);
            if (tx != null) _transactions.Remove(tx);
            return Task.CompletedTask;
        }
    }
}
