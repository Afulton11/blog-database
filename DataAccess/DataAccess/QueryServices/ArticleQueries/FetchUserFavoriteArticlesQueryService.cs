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
    public class FetchUserFavoriteArticlesQueryService : IFetchUserFavoriteArticleQueryService
    {
        private readonly IDatabase database;
        private readonly IReader<Article> articleReader;

        public FetchUserFavoriteArticlesQueryService(IDatabase database, IReader<Article> articleReader)
        {
            EnsureArg.IsNotNull(database, nameof(database));
            EnsureArg.IsNotNull(articleReader, nameof(articleReader));

            this.database = database;
            this.articleReader = articleReader;
        }

        public Paged<Article> Execute(FetchUserFavoriteArticlesQuery query)
        {
            return database.TryExecuteTransaction((transaction) =>
            {
                var procedure = database.CreateStoredProcCommand("Blog.GetFavoriteArticles", transaction);

                procedure.Parameters.AddAll(GetParameters(query));

                return new Paged<Article>
                {
                    Paging = query.Paging,
                    Items = database.ExecuteReader(procedure, articleReader.Read),
            };

            });
        }

        private IEnumerable<IDataParameter> GetParameters(FetchUserFavoriteArticlesQuery query)
        {
            yield return database.CreateParameter("@UserId", query.UserId);
            yield return database.CreateParameter("@PageNumber", query.Paging.PageIndex);
            yield return database.CreateParameter("@PageSize", query.Paging.PageSize);
        }
    }
}
