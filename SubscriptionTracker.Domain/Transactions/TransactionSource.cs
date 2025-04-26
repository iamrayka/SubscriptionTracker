// ------------------------------------------------------------------------------
//  Project: SubscriptionTracker.Domain
//  File: TransactionSource.cs
//  Created: 25/04/2025  by Rayka
//  Description: Defines the source type of a transaction (manual, CSV import, recurring pattern).
// ------------------------------------------------------------------------------
namespace SubscriptionTracker.Domain.Transactions
{
    /// <summary>
    /// Represents the source of the transaction data.
    /// </summary>
    public enum TransactionSource
    {
        Manual, // Manually entered by the user
        CsvImport, // Imported from a CSV file
        BankFeed, // Imported directly from a bank feed
        RecurringPattern // Generated from a recurring pattern
    }
}
