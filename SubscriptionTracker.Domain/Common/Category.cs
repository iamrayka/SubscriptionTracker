// ------------------------------------------------------------------------------
//  Project: SubscriptionTracker.Domain
//  File: Category.cs
//  Created: 25/04/2025 by Rayka
//  Description: Defines the Category entity for classifying financial transactions dynamically.
// ------------------------------------------------------------------------------
namespace SubscriptionTracker.Domain.Common
{
    /// <summary>
    /// Represents a category that can be assigned to financial transactions.
    /// </summary>
    public class Category
    {
        /// <summary>
        /// Unique identifier for the category.
        /// </summary>
        public Guid Id { get; private set; }

        /// <summary>
        /// Name of the category (e.g., Groceries, Dining, Transportation).
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Optional color for the category tag in UI.
        /// </summary>
        public string Color { get; private set; }

        /// <summary>
        /// Private parameterless constructor for ORM or serialization purposes.
        /// </summary>
        private Category() { }

        /// <summary>
        /// Creates a new Category with a specified name and optional color.
        /// </summary>
        /// <param name="sName">Name of the category.</param>
        /// <param name="sColor">Optional color code for UI (default is grey).</param>
        public Category(string sName, string sColor = "#CCCCCC")
        {
            if (string.IsNullOrWhiteSpace(sName))
            {
                throw new ArgumentException("Category name cannot be empty.", nameof(sName));
            }

            Id = Guid.NewGuid();
            this.Name = sName.Trim();
            this.Color = sColor;
        }
    }
}
