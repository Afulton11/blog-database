using Common;
using DataAccess.QueryServices.Readers;
using Domain.Entities.View;
using System;
using System.Data;

namespace DataAccess.DataAccess.QueryServices.Readers
{
    public class ArticleCommentReader : Reader<ArticleComment>
    {
        protected override ArticleComment ReadRow(IDataRecord row) =>
            new ArticleComment
            {
                CommentId = row.GetSafely<int>(nameof(ArticleComment.CommentId)),
                ParentCommentId = row.GetSafely<int>(nameof(ArticleComment.ParentCommentId)),
                UserId = row.GetSafely<int>(nameof(ArticleComment.UserId)),
                Username = row.GetSafely<string>(nameof(ArticleComment.Username)),
                Body = row.GetSafely<string>(nameof(ArticleComment.Body)),
                CreationDateTime = row.GetSafely<DateTime>(nameof(ArticleComment.CreationDateTime)),
                DeletedAt = row.GetSafely<DateTime>(nameof(ArticleComment.DeletedAt)),
            };
    }
}
