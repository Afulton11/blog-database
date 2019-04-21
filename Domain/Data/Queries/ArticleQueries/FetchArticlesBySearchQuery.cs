using Domain.Entities.Blog;
using System.ComponentModel.DataAnnotations;

namespace Domain.Data.Queries.ArticleQueries
{
    public class FetchArticlesBySearchQuery : IPaginatedQuery<Article>
    {
        [Required(AllowEmptyStrings = false)]
        [MaxLength(510)]
        public string SearchText { get; set; }
        public PageInfo Paging { get; set; } = new PageInfo();
    }
}
