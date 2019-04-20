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

namespace DataAccess.DataAccess.QueryServices.CommentQueries
{
    public class FetchUserCommentsQueryService : IFetchUserCommentsQueryService
    {
        private readonly IDatabase database;
        private readonly IReader<Comment> commentReader;

        public FetchUserCommentsQueryService(IDatabase database, IReader<Comment> commentReader)
        {
            EnsureArg.IsNotNull(database, nameof(database));
            EnsureArg.IsNotNull(commentReader, nameof(commentReader));
            this.database = database;
            this.commentReader = commentReader;
        }

        public Paged<Comment> Execute(FetchUserCommentsQuery query)
        {
            return database.TryExecuteTransaction((transaction) =>
            {
                var procedure = database.CreateStoredProcCommand("Blog.GetUserComments", transaction);
                procedure.Parameters.AddAll(GetParameters(query));

                return new Paged<Comment>
                {
                    Paging = query.Paging,
                    Items = database.ExecuteReader(procedure, commentReader.Read),
                };
            });
        }

        private IEnumerable<IDataParameter> GetParameters(FetchUserCommentsQuery query)
        {
            yield return database.CreateParameter("@UserId", query.UserId);
            yield return database.CreateParameter("@PageNumber", query.Paging.PageIndex);
            yield return database.CreateParameter("@PageSize", query.Paging.PageSize);
        }
    }
}
