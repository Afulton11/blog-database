using Common;
using DataAccess.QueryServices.Readers;
using Domain.Entities.Blog;
using System;
using System.Data;

namespace DataAccess.DataAccess.QueryServices.Readers
{
    public class ArticleCategoryReader : Reader<ArticleCategory>
    {
        protected override ArticleCategory ReadRow(IDataRecord row) =>
            new ArticleCategory
            {
                ArticleCategoryID = row.GetSafely<int>(nameof(ArticleCategory.ArticleCategoryID)),
                CreationDateTime = row.GetSafely<DateTime>(nameof(ArticleCategory.CreationDateTime)),
                CreationUserID = row.GetSafely<int>(nameof(ArticleCategory.CreationUserID)),
                LastUpdateDateTime = row.GetSafely<DateTime>(nameof(ArticleCategory.LastUpdateDateTime)),
            };
    }
}
