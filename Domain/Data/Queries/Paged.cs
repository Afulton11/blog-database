using DataAccess.DataAccess.QueryServices;
using System.Collections.Generic;

namespace Domain.Data.Queries
{
    /// <summary>
    /// Contains a set of items for the requested page.
    /// 
    /// For example usage see <see cref="FetchUserFavoriteArticlesQuery"/>
    /// 
    /// </summary>
    /// <typeparam name="TItem">The item type</typeparam>
    public class Paged<TItem>
    {
        public PageInfo Paging { get; set; }
        public IEnumerable<TItem> Items { get; set; }
    }
}
