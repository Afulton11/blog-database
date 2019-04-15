using Core.Entities.Blog;
using System.Collections.Generic;

namespace Mocker.Comparers
{
    public class ArticleEqualityComparer : IEqualityComparer<Article>
    {
        public bool Equals(Article x, Article y) =>
            x.ArticleId == y.ArticleId
            && x.AuthorId == y.AuthorId
            && x.CategoryId == y.CategoryId
            && (x.ContentStatus?.Equals(y.ContentStatus) ?? true)
            && x.Title == y.Title
            && x.Body == y.Body
            && x.CreationDateTime == y.CreationDateTime
            && x.LastUpdateDateTime == y.LastUpdateDateTime;

        public int GetHashCode(Article obj) => obj.GetHashCode();
    }
}
