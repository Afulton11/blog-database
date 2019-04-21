using System.Collections.Generic;
using System.Data;
using System.Linq;
using DataAccess.QueryServices.Readers;
using DatabaseFactory.Data.Contracts;
using Domain.Business.QueryServices.ArticleCategoryQueryServices;
using Domain.Data.Queries.ArticleCategoryQueries;
using Domain.Entities.Blog;
using EnsureThat;

namespace DataAccess.QueryServices.ArticleCategoryQueries
{
    public class FetchArticleCategoryQueryService :
        DbQueryService<FetchArticleCategoryQuery, ArticleCategory>,
        IFetchArticleCategoryQueryService
    {
        private readonly IReader<ArticleCategory> articleCategoryReader;

        public FetchArticleCategoryQueryService(IDatabase database, IReader<ArticleCategory> articleCategoryReader)
            : base(database)
        {
            EnsureArg.IsNotNull(articleCategoryReader, nameof(articleCategoryReader));
            this.articleCategoryReader = articleCategoryReader;
        }

        protected override ArticleCategory ReadQueryResult(IDataReader reader, FetchArticleCategoryQuery query) =>
            articleCategoryReader.Read(reader)?.FirstOrDefault() ?? null;

        protected override IEnumerable<IDataParameter> GetParameters(FetchArticleCategoryQuery query)
        {
            yield return Database.CreateParameter("@ArticleCategoryId", query.ArticleCategoryId);
        }

        protected override string ProcedureName => "Blog.GetArticleCategory";
    }
}
