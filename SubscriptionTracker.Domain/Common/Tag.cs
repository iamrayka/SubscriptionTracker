// ------------------------------------------------------------------------------
//  Project: SubscriptionTracker.Domain
//  File: Tag.cs
//  Created: 25/04/2025 by Rayka
//  Description: Defines the Tag entity used to label and categorize transactions flexibly.
// ------------------------------------------------------------------------------

using System;

namespace SubscriptionTracker.Domain.Common
{
    /// <summary>
    /// Represents a tag that can be assigned to transactions for flexible categorization.
    /// </summary>
    public class Tag
    {
        /// <summary>
        /// Unique identifier for the tag.
        /// </summary>
        public Guid Id { get; private set; }

        /// <summary>
        /// Name of the tag (e.g., "Recurring", "Business", "Personal").
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Optional color code for the tag (for UI purposes).
        /// </summary>
        public string Color { get; private set; }

        /// <summary>
        /// Private parameterless constructor for ORM or serialization purposes.
        /// </summary>
        private Tag() { }

        /// <summary>
        /// Creates a new Tag with a specified name and optional color.
        /// </summary>
        /// <param name="sName">Name of the tag.</param>
        /// <param name="sColor">Optional color code (default grey).</param>
        public Tag(string sName, string sColor = "#CCCCCC")
        {
            if (string.IsNullOrWhiteSpace(sName))
            {
                throw new ArgumentException("Tag name cannot be empty.", nameof(sName));
            }

            Id = Guid.NewGuid();
            this.Name = sName.Trim();
            this.Color = sColor;
        }
    }
}
