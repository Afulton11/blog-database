using DataAccess.QueryServices;
using DatabaseFactory.Data.Contracts;
using Domain.Data.Queries;
using System.Collections.Generic;
using System.Data;

namespace DataAccess.DataAccess.QueryServices
{
    public abstract class DbPagedQueryService<TQuery, TPageItem> : DbQueryService<TQuery, Paged<TPageItem>>
        where TQuery : IPaginatedQuery<TPageItem>
    {
        public DbPagedQueryService(IDatabase database) : base(database)
        {
        }

        protected override Paged<TPageItem> ReadQueryResult(IDataReader reader, TQuery query)
        {
            return new Paged<TPageItem>
            {
                Paging = query.Paging,
                Items = ReadItems(reader),
            };
        }

        protected abstract IEnumerable<TPageItem> ReadItems(IDataReader dataReader);
    }
}
