using Core.Entities.Blog;
using System.Collections.Generic;

namespace Mocker.Comparers
{
    public class ArticleEqualityComparer : IEqualityComparer<Article>
    {
        public bool Equals(Article x, Article y) =>
            x.ArticleID == y.ArticleID
            && x.AuthorID == y.AuthorID
            && x.CategoryID == y.CategoryID
            && (x.ContentStatus?.Equals(y.ContentStatus) ?? true)
            && x.Title == y.Title
            && x.Body == y.Body
            && x.CreationDateTime == y.CreationDateTime
            && x.LastUpdateDateTime == y.LastUpdateDateTime;

        public int GetHashCode(Article obj) => obj.GetHashCode();
    }
}
