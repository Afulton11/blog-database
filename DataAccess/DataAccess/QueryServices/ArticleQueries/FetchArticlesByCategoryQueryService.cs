using DataAccess.QueryServices.Readers;
using DatabaseFactory.Data.Contracts;
using Domain.Business.QueryServices.ArticleQueryServices;
using Domain.Data.Queries.ArticleQueries;
using Domain.Entities.Blog;
using EnsureThat;
using System.Collections.Generic;
using System.Data;

namespace DataAccess.DataAccess.QueryServices.ArticleQueries
{
    public class FetchArticlesByCategoryQueryService :
        DbPagedQueryService<FetchArticlesByCategoryQuery, Article>,
        IFetchArticlesByCategoryQueryService
    {
        private readonly IReader<Article> articleReader;
        public FetchArticlesByCategoryQueryService(IDatabase database, IReader<Article> articleReader)
            : base(database)
        {
            EnsureArg.IsNotNull(articleReader, nameof(articleReader));

            this.articleReader = articleReader;
        }

        protected override IEnumerable<Article> ReadItems(IDataReader dataReader) =>
            articleReader.Read(dataReader);

        protected override IEnumerable<IDataParameter> GetQueryParameters(FetchArticlesByCategoryQuery query)
        {
            yield return Database.CreateParameter("@CategoryId", query.ArticleCategoryId);
        }

        protected override string ProcedureName => "Blog.GetArticlesByCategory";
    }
}
