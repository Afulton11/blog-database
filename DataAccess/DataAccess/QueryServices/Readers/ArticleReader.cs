using Common;
using Domain.Entities.Blog;
using System;
using System.Data;

namespace DataAccess.QueryServices.Readers
{
    /// <summary>
    /// Reads all articles found in the given <see cref="IDataReader"/>
    /// </summary>
    public class ArticleReader : Reader<Article>
    {
        protected override Article ReadRow(IDataRecord row) =>
            new Article
            {
                ArticleId = row.GetSafely<int>(nameof(Article.ArticleId)),
                AuthorId = row.GetSafely<int>(nameof(Article.AuthorId)),
                CategoryId = row.GetSafely<int>(nameof(Article.CategoryId)),
                ContentStatus = row.GetSafely<ContentStatus>(nameof(Article.ContentStatus)),
                Title = row.GetSafely<string>(nameof(Article.Title)),
                Description = row.GetSafely<string>(nameof(Article.Description)),
                Body = row.GetSafely<string>(nameof(Article.Body)),
                CreationDateTime = row.GetSafely<DateTime>(nameof(Article.CreationDateTime)),
                LastUpdateDateTime = row.GetSafely<DateTime>(nameof(Article.LastUpdateDateTime)),
            };
    }
}
