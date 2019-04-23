using DatabaseFactory.Data.Contracts;
using Domain.Business.CommandServices.Comments;
using Domain.Data.Commands.Comments;
using EnsureThat;
using System.Collections.Generic;
using System.Data;

namespace DataAccess.CommandServices.Comments
{
    public class CreateOrUpdateCommentCommandService : ICreateCommentCommandService
    {
        private readonly IDatabase database;

        public CreateOrUpdateCommentCommandService(IDatabase database)
        {
            EnsureArg.IsNotNull(database, nameof(database));

            this.database = database;
        }

        public void Execute(CreateOrUpdateCommentCommand command)
        {
            EnsureArg.IsNotNull(command, nameof(command));

            database.TryExecuteTransaction((transaction) =>
            {
                var dbCommand = database.CreateStoredProcCommand("Blog.CreateOrUpdateComment", transaction);
                var parameters = CreateParameters(command);

                foreach (var p in parameters)
                {
                    dbCommand.Parameters.Add(p);
                }

                database.Execute(dbCommand);
            });
        }

        private IEnumerable<IDataParameter> CreateParameters(CreateOrUpdateCommentCommand command)
        {
            yield return database.CreateParameter("@UserId", command.UserID);
            yield return database.CreateParameter("@ArticleId", command.ArticleID);
            yield return database.CreateParameter("@Body", command.Body);

            if (command.ParentCommentID != null)
            {
                yield return database.CreateParameter("@ParentCommentId", command.ParentCommentID);
            }

        }
    }
}
