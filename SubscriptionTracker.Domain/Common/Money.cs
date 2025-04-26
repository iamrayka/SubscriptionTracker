// ------------------------------------------------------------------------------
//  Project: SubscriptionTracker.Domain
//  File: Money.cs
//  Created: 25/04/2025 by Rayka
//  Description: Defines the Money value object for handling monetary amounts safely.
// ------------------------------------------------------------------------------
namespace SubscriptionTracker.Domain.Common
{
    /// <summary>
    /// Represents a monetary value including amount and currency.
    /// </summary>
    public class Money
    {
        /// <summary>
        /// The amount of money.
        /// </summary>
        public decimal Amount { get; private set; }

        /// <summary>
        /// The ISO currency code (e.g., GBP, USD, EUR).
        /// </summary>
        public string Currency { get; private set; }

        /// <summary>
        /// Private constructor for ORM or serialization purposes.
        /// </summary>
        private Money() { }

        /// <summary>
        /// Creates a new Money object.
        /// </summary>
        /// <param name="dAmount">Amount of money.</param>
        /// <param name="sCurrency">Currency code (ISO format).</param>
        public Money(decimal dAmount, string sCurrency)
        {
            if (string.IsNullOrWhiteSpace(sCurrency))
            {
                throw new ArgumentException("Currency cannot be null or empty.", nameof(sCurrency));
            }

            this.Amount = dAmount;
            this.Currency = sCurrency.ToUpperInvariant();
        }
    }
}
