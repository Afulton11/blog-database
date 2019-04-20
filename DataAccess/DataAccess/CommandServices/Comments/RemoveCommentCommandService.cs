using Common;
using DatabaseFactory.Data.Contracts;
using Domain.Business.CommandServices.Comments;
using Domain.Data.Commands.Comment;
using EnsureThat;
using System.Collections.Generic;
using System.Data;

namespace DataAccess.CommandServices.Comments
{
    public class RemoveCommentCommandService : IRemoveCommentCommandService
    {
        private readonly IDatabase database;

        public RemoveCommentCommandService(IDatabase database)
        {
            EnsureArg.IsNotNull(database);

            this.database = database;
        }
        
        public void Execute(RemoveCommentCommand command)
        {
            database.TryExecuteTransaction((transaction) =>
            {
                var procedure = database.CreateStoredProcCommand("Blog.RemoveComment", transaction);
                procedure.Parameters.AddAll(GetParameters(command));

                database.Execute(procedure);
            });
        }

        private IEnumerable<IDataParameter> GetParameters(RemoveCommentCommand command)
        {
            yield return database.CreateParameter("@CommentId", command.CommentId);
        }
    }
}
