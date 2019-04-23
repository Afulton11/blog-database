using Domain.Data;
using System;

namespace Domain.Entities.Blog
{
    public class Comment : IEntity
    {
        public int CommentID { get; set; }
        public int ParentCommentID { get; set; }
        public int UserID { get; set; }
        public int ArticleID { get; set; }
        public string Body { get; set; }
        public DateTime CreationDateTime { get; set; }
        public DateTime LastUpdateDateTime { get; set; }
        public DateTime DeletedAt { get; set; }
    }
}
