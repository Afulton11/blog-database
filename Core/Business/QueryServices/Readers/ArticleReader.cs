using Common;
using Core.Business.QueryServices.Readers;
using Core.Entities.Blog;
using System;
using System.Data;

namespace Core.Business.QueryServices.DataReaders
{
    /// <summary>
    /// Reads all articles found in the given <see cref="IDataReader"/>
    /// </summary>
    public class ArticleReader : Reader<Article>
    {
        protected override Article ReadRow(IDataRecord row) =>
            new Article
            {
                ArticleID = row.GetSafely<int>(nameof(Article.ArticleID)),
                AuthorID = row.GetSafely<int>(nameof(Article.AuthorID)),
                CategoryID = row.GetSafely<int>(nameof(Article.CategoryID)),
                ContentStatus = row.GetSafely<ContentStatus>(nameof(Article.ContentStatus)),
                Title = row.GetSafely<string>(nameof(Article.Title)),
                Description = row.GetSafely<string>(nameof(Article.Description)),
                Body = row.GetSafely<string>(nameof(Article.Body)),
                CreationDateTime = row.GetSafely<DateTimeOffset>(nameof(Article.CreationDateTime)),
                LastUpdateDateTime = row.GetSafely<DateTimeOffset>(nameof(Article.LastUpdateDateTime)),
            };
    }
}
