using Domain.Data.Queries;
using Domain.Data.Queries.ArticleQueries;
using Domain.Entities.View;
using EnsureThat;

namespace Domain.Business.QueryServices.ArticleQueryServices
{
    public class FetchRecentArticlesQueryService : IQueryService<FetchRecentArticlesPreviewQuery, Paged<PreviewArticle>>
    {
        private readonly IAsyncQueryProcessor queryProcessor;
       

        public FetchRecentArticlesQueryService(
            IAsyncQueryProcessor queryProcessor)
        {
            EnsureArg.IsNotNull(queryProcessor, nameof(queryProcessor));

            this.queryProcessor = queryProcessor;
        }


        public Paged<PreviewArticle> Execute(FetchRecentArticlesPreviewQuery query)
        {
            
        }
    }
}
