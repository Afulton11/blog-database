using Common;
using DataAccess.QueryServices.Readers;
using Domain.Entities.View;
using System;
using System.Data;

namespace DataAccess.DataAccess.QueryServices.Readers
{
    public class ArticleViewModelReader : Reader<ArticleViewModel>
    {
        protected override ArticleViewModel ReadRow(IDataRecord row) =>
            new ArticleViewModel
            {
                ArticleId = row.GetSafely<int>(nameof(ArticleViewModel.ArticleId)),
                AuthorUserId = row.GetSafely<int>(nameof(ArticleViewModel.AuthorUserId)),
                AuthorFullName = row.GetSafely<string>(nameof(ArticleViewModel.AuthorFullName)),
                CategoryName = row.GetSafely<string>(nameof(ArticleViewModel.CategoryName)),
                CreationDateTime = row.GetSafely<DateTime>(nameof(ArticleViewModel.CreationDateTime)),
                Title = row.GetSafely<string>(nameof(ArticleViewModel.Title)),
                Body = row.GetSafely<string>(nameof(ArticleViewModel.Body)),
                DidUserFavorite = row.GetSafely<bool>(nameof(ArticleViewModel.DidUserFavorite)),
                CommentCount = row.GetSafely<int>(nameof(ArticleViewModel.CommentCount)),
                FavoriteCount = row.GetSafely<int>(nameof(ArticleViewModel.FavoriteCount)),
                NextArticleId = row.GetSafely<int?>(nameof(ArticleViewModel.NextArticleId)),
                NextArticleTitle = row.GetSafely<string>(nameof(ArticleViewModel.NextArticleTitle)),
                PreviousArticleId = row.GetSafely<int?>(nameof(ArticleViewModel.PreviousArticleId)),
                PreviousArticleTitle = row.GetSafely<string>(nameof(ArticleViewModel.PreviousArticleTitle))
            };
    }
}
