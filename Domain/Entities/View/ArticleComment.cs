using Domain.Data;
using System;
namespace Domain.Entities.View
{
    public class ArticleComment : IEntity
    {
        public int CommentId { get; set; }
        public int? ParentCommentId { get; set; }
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Body { get; set; }
        public int Depth { get; set; }
        public DateTime CreationDateTime { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
