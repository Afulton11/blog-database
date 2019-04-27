using Domain.Data;
using System;

namespace Domain.Entities.View
{
    public class ArticleViewModel : IEntity
    {
        public int ArticleId { get; set; }
        public int AuthorUserId { get; set; }
        public string AuthorFullName { get; set; }
        public string CategoryName { get; set; }
        public DateTime CreationDateTime { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public bool DidUserFavorite { get; set; }
        public int CommentCount { get; set; }
        public int FavoriteCount { get; set; }
    }
}
