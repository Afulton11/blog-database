﻿using System.Collections.Generic;
using System.Data;
using DatabaseFactory.Data.Contracts;
using Domain.Business.CommandServices;
using Domain.Data.Commands;
using EnsureThat;

namespace DataAccess.CommandServices
{
    public class CreateFollowerCommandService : ICommandService<CreateFollowerCommand>
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
                var dbCommand = database.CreateStoredProcCommand("Blog.CreateFollower", transaction);
                var parameters = CreateParameters(command);

                foreach (var p in parameters)
                {
                    dbCommand.Parameters.Add(p);
                }

                database.Execute(dbCommand);
            });
        }

        private IEnumerable<IDataParameter> CreateParameters(CreateFollowerCommand command)
        {
            yield return database.CreateParameter("FollowerUserId", command.FollowedUserId);
            yield return database.CreateParameter("FollowingUserId", command.FollowingUserId);
        }
    }
}
