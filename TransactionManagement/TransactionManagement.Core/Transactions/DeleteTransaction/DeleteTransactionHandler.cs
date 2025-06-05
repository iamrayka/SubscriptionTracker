using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransactionManagement.Core.Transactions.DeleteTransaction
{
    public class DeleteTransactionHandler
    {
        private readonly ITransactionRepository _repository;

        public DeleteTransactionHandler(ITransactionRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> HandleAsync(Guid id)
        {
            var transaction = await _repository.GetByIdAsync(id);
            if (transaction == null) return false;

            await _repository.DeleteAsync(id);
            return true;
        }
    }
}
