using Domain.Data.Queries;
using Domain.Data.Queries.ArticleQueries;
using Domain.Data.Queries.AuthorQueries;
using Domain.Entities.Blog;
using Domain.Entities.View;
using EnsureThat;
using System;
using System.Collections.Generic;

namespace Domain.Business.QueryServices.ArticleQueryServices
{
    public class FetchRecentArticlesQueryService : IQueryService<FetchRecentArticlesPreviewQuery, Paged<PreviewArticle>>
    {
        private readonly IQueryProcessor queryProcessor;
       

        public FetchRecentArticlesQueryService(
            IQueryProcessor queryProcessor)
        {
            EnsureArg.IsNotNull(queryProcessor, nameof(queryProcessor));

            this.queryProcessor = queryProcessor;
        }


        public Paged<PreviewArticle> Execute(FetchRecentArticlesPreviewQuery query)
        {
            var pagedArticles = queryProcessor.Execute(new FetchRecentArticlesQuery
            {
                Paging = query.Paging
            });

            return new Paged<PreviewArticle>
            {
                Paging = query.Paging,
                Items = GetPreviewArticles(pagedArticles.Items)
            };
        }

        private IEnumerable<PreviewArticle> GetPreviewArticles(IEnumerable<Article> articles)
        {
            foreach (var article in articles)
            {
                var author = queryProcessor.Execute(new FetchAuthorByIdQuery
                {
                    AuthorId = article.AuthorId
                });

                yield return new PreviewArticle
                {
                    ArticleId = article.ArticleId,
                    Author = author,
                    CreatedDateString = FormateDateTime(article.CreationDateTime),
                    Description = article.Description,
                    Title = article.Title
                };
            }
        }

        private string FormateDateTime(DateTime date)
        {
            const string format = "MMMM dd, yyyy";
            return date.ToString(format);
        }
    }
}
