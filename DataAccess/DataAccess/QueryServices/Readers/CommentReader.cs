using Common;
using DataAccess.QueryServices.Readers;
using Domain.Entities.Blog;
using System;
using System.Data;

namespace DataAccess.DataAccess.QueryServices.Readers
{
    public class CommentReader : Reader<Comment>
    {
        protected override Comment ReadRow(IDataRecord row) =>
            new Comment
            {
                CommentID = row.GetSafely<int>(nameof(Comment.CommentID)),
                ParentCommentID = row.GetSafely<int>(nameof(Comment.ParentCommentID)),
                UserID = row.GetSafely<int>(nameof(Comment.UserID)),
                ArticleID = row.GetSafely<int>(nameof(Comment.ArticleID)),
                Body = row.GetSafely<string>(nameof(Comment.Body)),
                CreationDateTime = row.GetSafely<DateTime>(nameof(Comment.CreationDateTime)),
                LastUpdateDateTime = row.GetSafely<DateTime>(nameof(Comment.LastUpdateDateTime)),
                DeletedAt = row.GetSafely<DateTime>(nameof(Comment.DeletedAt))
            };
    }
}
