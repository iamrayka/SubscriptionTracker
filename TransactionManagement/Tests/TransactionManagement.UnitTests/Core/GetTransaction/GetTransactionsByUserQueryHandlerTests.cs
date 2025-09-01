using Moq;
using FluentAssertions;
using TransactionManagement.Core.Transactions;
using TransactionManagement.Core.Transactions.GetTransaction;

namespace TransactionManagement.Tests.GetTransaction;

/// <summary>
/// Unit tests for GetTransactionsByUserQueryHandler.
/// These tests verify the query behavior for retrieving transactions
/// by user ID and ensure appropriate handling of edge cases.
/// </summary>
public class GetTransactionsByUserQueryHandlerTests
{
    /// <summary>
    /// Test: Valid user ID should return a list of transactions.
    /// Verifies that:
    /// - The repository is called with the correct user ID
    /// - A successful result is returned
    /// - The result contains the expected number of transactions
    /// </summary>
    [Fact]
    public async Task HandleAsync_ShouldReturnTransactions_WhenUserExists()
    {
        // Arrange
        var userId = Guid.NewGuid();

        var transactions = new List<Transaction>
        {
            new(userId, DateTime.Today, "Test", 100m)
        };

        var repoMock = new Mock<ITransactionRepository>();
        repoMock.Setup(r => r.GetByUserIdAsync(userId)).ReturnsAsync(transactions);

        var handler = new GetTransactionsByUserQueryHandler(repoMock.Object);
        var query = new GetTransactionsByUserQuery { UserId = userId };

        // Act
        var result = await handler.HandleAsync(query);

        // Assert
        result.IsSuccess.Should().BeTrue("a valid user ID should return transactions");
        result.Value.Should().HaveCount(1, "one transaction should be returned for the mock data");
        result.Value.First().Description.Should().Be("Test", "the transaction description should match the mock setup");
    }

    /// <summary>
    /// Test: Empty user ID should return a failure result.
    /// Verifies that:
    /// - The handler validates input before accessing the repository
    /// - A failure result with an appropriate error message is returned
    /// </summary>
    [Fact]
    public async Task HandleAsync_ShouldReturnFailure_WhenUserIdIsEmpty()
    {
        // Arrange
        var handler = new GetTransactionsByUserQueryHandler(new Mock<ITransactionRepository>().Object);
        var query = new GetTransactionsByUserQuery { UserId = Guid.Empty };

        // Act
        var result = await handler.HandleAsync(query);

        // Assert
        result.IsFailure.Should().BeTrue("an empty user ID is invalid and should not be processed");
        result.Error.Should().Contain("User ID", "the error should indicate that the user ID is required");
    }
}