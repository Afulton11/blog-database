using Common;
using DataAccess.QueryServices.Readers;
using DatabaseFactory.Data.Contracts;
using Domain.Business.QueryServices.CommentQueryServices;
using Domain.Data.Queries;
using Domain.Data.Queries.CommentQueries;
using Domain.Entities.Blog;
using EnsureThat;
using System.Collections.Generic;
using System.Data;

namespace DataAccess.QueryServices.CommentQueries
{
    public class FetchArticleCommentsQueryService : IFetchArticleCommentsQueryService
    {
        private readonly IDatabase database;
        private readonly IReader<Comment> commentReader;

        public FetchArticleCommentsQueryService(IDatabase database, IReader<Comment> commentReader)
        {
            EnsureArg.IsNotNull(database, nameof(database));
            EnsureArg.IsNotNull(commentReader, nameof(commentReader));

            this.database = database;
            this.commentReader = commentReader;
        }

        public Paged<Comment> Execute(FetchArticleCommentsQuery query)
        {
            return database.TryExecuteTransaction((transaction) =>
            {
                var procedure = database.CreateStoredProcCommand("Blog.GetComments", transaction);
                procedure.Parameters.AddAll(GetParameters(query));

                return new Paged<Comment>
                {
                    Paging = query.Paging,
                    Items = database.ExecuteReader(procedure, commentReader.Read),
                };
            });
        }

        public IEnumerable<IDataParameter> GetParameters(FetchArticleCommentsQuery query)
        {
            yield return database.CreateParameter("@ArticleId", query.ArticleId);
            yield return database.CreateParameter("@PageNumber", query.Paging.PageIndex);
            yield return database.CreateParameter("@PageSize", query.Paging.PageSize);
        }
    }
}
