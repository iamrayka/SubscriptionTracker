using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransactionManagement.Core.Transactions.GetTransaction
{
    public class GetTransactionsByUserHandler
    {
        private readonly ITransactionRepository _repository;

        public GetTransactionsByUserHandler(ITransactionRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Transaction>> HandleAsync(Guid userId)
        {
            return await _repository.GetByUserIdAsync(userId);
        }
    }
}
