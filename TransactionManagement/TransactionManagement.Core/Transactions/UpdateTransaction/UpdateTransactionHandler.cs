using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransactionManagement.Core.Transactions.UpdateTransaction
{
    public class UpdateTransactionHandler
    {
        private readonly ITransactionRepository _repository;

        public UpdateTransactionHandler(ITransactionRepository repository)
        {
            _repository = repository;
        }

        public async Task<Transaction?> HandleAsync(Guid id, UpdateTransactionRequest request)
        {
            var transaction = await _repository.GetByIdAsync(id);
            if (transaction == null) return null;

            transaction.Update(request.TransactionDate, request.Description, request.Amount);
            await _repository.UpdateAsync(transaction);
            return transaction;
        }
    }
}
