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

        protected override IEnumerable<IDataParameter> GetParameters(TQuery query)
        {
            foreach (var parameter in GetQueryParameters(query))
            {
                yield return parameter;
            }
            foreach (var parameter in GetPagingParameters(query))
            {
                yield return parameter;
            }
        }

        protected abstract IEnumerable<IDataParameter> GetQueryParameters(TQuery query);

        protected virtual IEnumerable<IDataParameter> GetPagingParameters(TQuery query)
        {
            var paging = query.Paging;
            yield return Database.CreateParameter("@PageNumber", paging.PageIndex);
            yield return Database.CreateParameter("@PageSize", paging.PageSize);
        }
    }
}
