using EnsureThat;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Entities.Blog
{
    /// <summary>
    /// An enum-like class that represents the different Content Statuses found within our database.
    /// This entity contains a limited set of values because it is static within our database.
    /// </summary>
    public class ContentStatus
    {
        private ContentStatus(string value) { Value = value; }
        public string Value { get; set; }

        public static ContentStatus Draft => new ContentStatus("Draft");
        public static ContentStatus Published => new ContentStatus("Published");
        public static ContentStatus Deleted => new ContentStatus("Deleted");
        public static ContentStatus Removed => new ContentStatus("Removed");
        public static ContentStatus Archived => new ContentStatus("Archived");

        public static IEnumerable<ContentStatus> AllStatuses
        {
            get
            {
                yield return Draft;
                yield return Published;
                yield return Deleted;
                yield return Removed;
                yield return Archived;
            }
        }

        /// <summary>
        /// Explicitly cast strings to a <see cref="ContentStatus"/>
        /// </summary>
        /// <param name="value">The <see cref="string"/> to convert.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="value"/> is 
        /// null,
        /// whitespace,
        /// or not in <see cref="ContentStatus.AllStatuses"/>
        /// </exception>
        public static explicit operator ContentStatus(string value)
        {
            EnsureArg.IsNotEmptyOrWhitespace(value, nameof(value));

            var result = AllStatuses.First((status) => status.Value == value);

            // TODO: Show custom exception here.
            EnsureArg.IsNotNull(result, nameof(value));

            return result;
        }

        public override bool Equals(object obj) =>
            (
                obj is string value
                && Value == value
            ) ||
            (
                obj is ContentStatus status
                && Value == status.Value
            );

        public override int GetHashCode() => HashCode.Combine(Value);
    }
}
