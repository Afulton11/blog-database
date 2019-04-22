using DataAccess.DataAccess.QueryServices;
using DataAccess.QueryServices.Readers;
using DatabaseFactory.Data.Contracts;
using Domain.Data.Queries.ArticleQueries;
using Domain.Entities.Blog;
using EnsureThat;
using System.Collections.Generic;
using System.Data;

namespace DataAccess.QueryServices.ArticleQueries
{
    public class FetchArticlesBySearchQueryService : DbPagedQueryService<FetchArticlesBySearchQuery, Article>
    {
        private readonly IReader<Article> articleReader;
        public FetchArticlesBySearchQueryService(IDatabase database, IReader<Article> articleReader) : base(database)
        {
            EnsureArg.IsNotNull(articleReader, nameof(articleReader));
            this.articleReader = articleReader;
        }

        protected override IEnumerable<Article> ReadItems(IDataReader dataReader) =>
            articleReader.Read(dataReader);

        protected override IEnumerable<IDataParameter> GetQueryParameters(FetchArticlesBySearchQuery query)
        {
            yield return Database.CreateParameter("@SearchText", query.SearchText);
        }

        protected override string ProcedureName => "Blog.SearchArticles";
    }
}
