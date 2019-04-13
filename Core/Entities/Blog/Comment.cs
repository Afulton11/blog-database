using Core.Data;
using System;

namespace Core.Entities.Blog
{
    public class Comment : IEntity
    {
        public int CommentID { get; set; }
        public int ParentCommentID { get; set; }
        public int UserID { get; set; }
        public int ArticleID { get; set; }
        public string Body { get; set; }
        public DateTimeOffset CreationDateTime { get; set; }
        public DateTimeOffset LastUpdateDateTime { get; set; }
        public DateTimeOffset DeletedAt { get; set; }
    }
}
