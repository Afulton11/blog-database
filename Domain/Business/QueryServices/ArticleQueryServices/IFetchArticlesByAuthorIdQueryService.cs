using System;
using Domain.Data.Queries;
using Domain.Entities.Blog;
using System.Collections.Generic;

namespace Domain.Business.QueryServices.ArticleQueryServices
{
    public interface IFetchArticlesByAuthorIdQueryService : IQueryService<FetchArticlesByAuthorIdQuery, Paged<Article>>
    {
    }
}
