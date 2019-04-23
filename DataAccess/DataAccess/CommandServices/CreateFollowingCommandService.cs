using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.DataAccess.CommandServices
{
    public class CreateFollowingCommandService : ICommandService<CreateFollowingCommand>
    {
        private readonly IDatabase database;

        public CreateFollowingCommandService(IDatabase database)
        {
            EnsureArg.IsNotNull(database, nameof(database));

            this.database = database;
        }

        public void Execute(CreateFollowingCommand command)
        {
            EnsureArg.IsNotNull(command, nameof(command));

            database.TryExecuteTransaction((transaction) =>
            {
                var dbCommand = database.CreateStoredProcCommand("Blog.CreateFollowing", transaction);
                var parameters = CreateParameters(command);

                foreach (var p in parameters)
                {
                    dbCommand.Parameters.Add(p);
                }

                database.Execute(dbCommand);
            });
        }

        private IEnumerable<IDataParameter> CreateParameters(CreateFollowingCommand command)
        {
            yield return database.CreateParameter("FollowerUserId", command.FollowedUserID);
            yield return database.CreateParameter("FollowingUserId", command.FollowingUserID);
        }
    }
}
