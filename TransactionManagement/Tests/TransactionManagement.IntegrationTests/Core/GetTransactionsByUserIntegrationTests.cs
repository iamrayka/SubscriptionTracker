using FluentAssertions;
using TransactionManagement.Core.Transactions;
using TransactionManagement.Core.Transactions.GetTransaction;
using TransactionManagement.Infrastructure.Transactions;

namespace TransactionManagement.IntegrationTests.Core.GetTransaction;

/// <summary>
/// Integration tests for GetTransactionsByUserQueryHandler using the real in-memory repository.
/// Verifies correct filtering, success on valid input, and failure on invalid input.
/// </summary>
public class GetTransactionsByUserIntegrationTests
{
    /// <summary>
    /// Test: When valid transactions exist for a given user, the handler should return only those transactions.
    /// </summary>
    [Fact]
    public async Task HandleAsync_ShouldReturnUserTransactions_WhenTheyExist()
    {
        // Arrange
        var repo = new TransactionRepository(); // Use in-memory implementation
        var userId = Guid.NewGuid();            // Simulate a user ID

        // Create two transactions for the test user
        var tx1 = new Transaction(userId, DateTime.Today, "User 1 - Tx1", 100);
        var tx2 = new Transaction(userId, DateTime.Today, "User 1 - Tx2", 150);

        // Create a transaction for another user (should not be returned)
        var otherUserTx = new Transaction(Guid.NewGuid(), DateTime.Today, "Other", 999);

        // Add all transactions to the repository
        await repo.CreateAsync(tx1);
        await repo.CreateAsync(tx2);
        await repo.CreateAsync(otherUserTx);

        // Create the query handler and prepare the query
        var handler = new GetTransactionsByUserQueryHandler(repo);
        var query = new GetTransactionsByUserQuery { UserId = userId };

        // Act: Execute the query to retrieve transactions for the user
        var result = await handler.HandleAsync(query);

        // Assert: Ensure the result indicates success
        result.IsSuccess.Should().BeTrue("transactions exist for the specified user");

        // Assert: Exactly two transactions should be returned for this user
        result.Value.Should().HaveCount(2, "only the 2 transactions for the user should be returned");

        // Assert: All returned transactions must belong to the queried user ID
        result.Value.Should().OnlyContain(t => t.UserId == userId, "all returned transactions must belong to the user");
    }

    /// <summary>
    /// Test: When an empty GUID is used as the UserId, the handler should fail validation.
    /// This protects the repository from being queried with invalid input.
    /// </summary>
    [Fact]
    public async Task HandleAsync_ShouldReturnFailure_WhenUserIdIsEmpty()
    {
        // Arrange: No need to prepopulate repository — input is invalid
        var repo = new TransactionRepository();
        var handler = new GetTransactionsByUserQueryHandler(repo);

        var query = new GetTransactionsByUserQuery
        {
            UserId = Guid.Empty // Invalid user ID
        };

        // Act: Attempt to run the query
        var result = await handler.HandleAsync(query);

        // Assert: Should return failure with a clear error message
        result.IsFailure.Should().BeTrue("an empty user ID is invalid input");
        result.Error.Should().Contain("User ID", "the error message should clearly indicate the missing input");
    }
}