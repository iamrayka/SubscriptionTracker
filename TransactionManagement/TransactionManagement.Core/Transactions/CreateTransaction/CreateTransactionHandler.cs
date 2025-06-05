using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransactionManagement.Core.Transactions.CreateTransaction
{
    public class CreateTransactionHandler
    {
        private readonly ITransactionRepository _repository;

        public CreateTransactionHandler(ITransactionRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<Transaction> HandleAsync(Guid userId, DateTime transactionDate, string description, decimal amount)
        {
            var transaction = new Transaction(userId, transactionDate, description, amount);
            await _repository.CreateAsync(transaction);
            return transaction;
        }
    }

}
