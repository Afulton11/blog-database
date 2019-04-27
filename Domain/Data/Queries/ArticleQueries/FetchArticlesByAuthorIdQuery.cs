using System;
using Domain.Entities.Blog;
using System.ComponentModel.DataAnnotations;

namespace Domain.Data.Queries
{
    public class FetchArticlesByAuthorIdQuery : IQuery<Paged<Article>>
    {
        [Required]
        public int AuthorId { get; set; }
        [Required]
        public PageInfo Paging { get; set; }
    }
}
