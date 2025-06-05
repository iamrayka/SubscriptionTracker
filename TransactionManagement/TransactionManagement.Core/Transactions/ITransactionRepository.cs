using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransactionManagement.Core.Transactions
{
    public interface ITransactionRepository
    {
        Task CreateAsync(Transaction transaction);
        Task<List<Transaction>> GetByUserIdAsync(Guid userId);
        Task<Transaction?> GetByIdAsync(Guid id);
        Task UpdateAsync(Transaction transaction);
        Task DeleteAsync(Guid id);
    }
}
