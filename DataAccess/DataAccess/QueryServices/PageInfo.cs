namespace DataAccess.DataAccess.QueryServices
{
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
