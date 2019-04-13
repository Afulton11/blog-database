using System;

namespace Core.Entities.Blog
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

        public override bool Equals(object obj) =>
            obj is ContentStatus status
            && Value == status.Value;

        public override int GetHashCode()=> HashCode.Combine(Value);
    }
}
