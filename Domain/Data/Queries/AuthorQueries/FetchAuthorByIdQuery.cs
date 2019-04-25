using Domain.Entities.Blog;
using System.ComponentModel.DataAnnotations;

namespace Domain.Data.Queries.AuthorQueries
{
    public class FetchAuthorByIdQuery : IQuery<Author>
    {
        [Required]
        public int AuthorId { get; set; }
    }
}
