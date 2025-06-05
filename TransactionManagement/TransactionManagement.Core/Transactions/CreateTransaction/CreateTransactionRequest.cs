namespace TransactionManagement.Core.Transactions.CreateTransaction;

public class CreateTransactionRequest
{
    public Guid UserId { get; set; }
    public DateTime TransactionDate { get; set; }
    public string Description { get; set; } = string.Empty;
    public decimal Amount { get; set; }
}
