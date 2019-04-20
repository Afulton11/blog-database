namespace Domain.Data.Queries
{
    /// <summary>
    /// Used for queries that have pagination
    /// </summary>
    public class PageInfo
    {
        /// <summary>
        /// The 0-based page index.
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// The number of items in a single page.
        /// </summary>
        public int PageSize { get; set; } = 15;
    }
}
