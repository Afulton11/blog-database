using System.Collections.Generic;
using System.Data;
using DatabaseFactory.Data.Contracts;
using Domain.Business.CommandServices;
using Domain.Data.Commands;
using EnsureThat;

namespace DataAccess.DataAccess.CommandServices
{
    public class CreateAuthorCommandService : ICommandService<CreateAuthorCommand>
    {
        private readonly IDatabase database;

        public CreateAuthorCommandService(IDatabase database)
        {
            EnsureArg.IsNotNull(database, nameof(database));

            this.database = database;
        }
        public void Execute(CreateAuthorCommand command)
        {
            EnsureArg.IsNotNull(command, nameof(command));

            database.TryExecuteTransaction((transaction) =>
            {
                var dbCommand = database.CreateStoredProcCommand("Blog.CreateAuthor", transaction);
                var parameters = CreateParameters(command);

                foreach (var p in parameters)
                {
                    dbCommand.Parameters.Add(p);
                }

                database.Execute(dbCommand);
            });
        }

        private IEnumerable<IDataParameter> CreateParameters(CreateAuthorCommand command)
        {
            yield return database.CreateParameter("AuthorUserId", command.AuthorUserID);
        }
    }
}
