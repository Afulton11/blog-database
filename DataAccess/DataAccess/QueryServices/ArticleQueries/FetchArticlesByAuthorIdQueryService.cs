using Common;
using DataAccess.QueryServices.Readers;
using DatabaseFactory.Data.Contracts;
using Domain.Business.QueryServices.ArticleQueryServices;
using Domain.Data.Queries;
using Domain.Entities.Blog;
using EnsureThat;
using System.Collections.Generic;
using System.Data;

namespace DataAccess.QueryServices.ArticleQueries
{
    public class FetchArticlesByAuthorIdQueryService : IFetchArticlesByAuthorIdQueryService
    {
        private readonly IDatabase Database;
        private readonly IReader<Article> ArticleReader;

        public FetchArticlesByAuthorIdQueryService
            (
                IDatabase database, 
                IReader<Article> articleReader
            )
        {
            EnsureArg.IsNotNull(database, nameof(database));
            EnsureArg.IsNotNull(articleReader, nameof(articleReader));

            Database = database;
            ArticleReader = articleReader;
        }

        public Paged<Article> Execute(FetchArticlesByAuthorIdQuery query)
        {
            return Database.TryExecuteTransaction((transaction) =>
            {
                var procedure = Database.CreateStoredProcCommand("Blog.FetchArticlesByAuthorId", transaction);
                procedure.Parameters.AddAll(GetParameters(query));

                return new Paged<Article>
                {
                    Paging = query.Paging,
                    Items = Database.ExecuteReader(procedure, ArticleReader.Read)
                };
            });
        }

        private IEnumerable<IDataParameter> GetParameters(FetchArticlesByAuthorIdQuery query)
        {
            yield return Database.CreateParameter("@AuthorId", query.AuthorId);
            yield return Database.CreateParameter("@PageSize", query.Paging.PageSize);
            yield return Database.CreateParameter("@PageNumber", query.Paging.PageIndex);
        }
    }
}
