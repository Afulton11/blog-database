using Common;
using DataAccess.QueryServices.Readers;
using DatabaseFactory.Data.Contracts;
using Domain.Business.QueryServices;
using Domain.Data.Queries;
using Domain.Data.Queries.ArticleQueries;
using Domain.Entities.Blog;
using EnsureThat;
using System.Collections.Generic;
using System.Data;

namespace DataAccess.QueryServices.ArticleQueries
{
    public class FetchRecentArticlesQueryService : IQueryService<FetchRecentArticlesQuery, Paged<Article>>
    {
        private readonly IDatabase database;
        private readonly IReader<Article> articleReader;

        public FetchRecentArticlesQueryService(IDatabase database, IReader<Article> articleReader)
        {
            EnsureArg.IsNotNull(database, nameof(database));
            EnsureArg.IsNotNull(articleReader, nameof(articleReader));

            this.database = database;
            this.articleReader = articleReader;
        }

        public Paged<Article> Execute(FetchRecentArticlesQuery query)
        {
            return database.TryExecuteTransaction((transaction) =>
            {
                var procedure = database.CreateStoredProcCommand("Blog.GetRecentArticles", transaction);
                procedure.Parameters.AddAll(GetParameters(query));

                return new Paged<Article>
                {
                    Paging = query.Paging,
                    Items = database.ExecuteReader(procedure, articleReader.Read),
                };
            });
        }

        private IEnumerable<IDataParameter> GetParameters(FetchRecentArticlesQuery query)
        {
            yield return database.CreateParameter("@PageNumber", query.Paging.PageIndex);
            yield return database.CreateParameter("@PageSize", query.Paging.PageSize);
        }
    }
}
