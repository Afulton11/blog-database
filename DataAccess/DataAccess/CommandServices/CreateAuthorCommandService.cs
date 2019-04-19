using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.DataAccess.CommandServices
{
    public class CreateAuthorCommandService : ICommandService<CreateAuthorCommand>
    {
        private readonly IDatabase database;

        public CreateFollowerCommandService(IDatabase database)
        {
            EnsureArg.IsNotNull(database, nameof(database));

            this.database = database;
        }
        public void Execute(CreateFollowerCommand command)
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
            yield return database.CreateParameter("AuthorUserId", command.AuthorUserId);
        }
    }
}
