using Domain.Entities.View;
using System.ComponentModel.DataAnnotations;

namespace Domain.Data.Queries
{
    public class FetchArticlePageByIdQuery : IQuery<ArticleViewModel>
    {
        [Required]
        public int ArticleId { get; set; }
        public int? UserId { get; set; }
    }
}
