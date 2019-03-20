using System;
using System.Linq;

namespace Blog.Core.BusinessLayer
{
    public static class PagingUtils
    {
        public static IQueryable<T> Paging<T>(this IQueryable<T> query, int pageSize, int pageNumber)
        {
            return query
                .Skip(pageSize * pageNumber)
                .Take(pageNumber);
        }
    }
}
