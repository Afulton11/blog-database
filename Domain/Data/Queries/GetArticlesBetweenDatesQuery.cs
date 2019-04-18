using Domain.Entities.Blog;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.Data.Queries
{
    public class GetArticlesBetweenDatesQuery : IQuery<IEnumerable<Article>>
    {
        [Required]
        public int ArticleID { get; set; }
        [Required]
        public DateTimeOffset StartDate { get; set; }
        [Required]
        public DateTimeOffset EndDate { get; set; }
    }
}
