﻿using Core.Business.Contracts;
using Core.Data.Commands;
using DatabaseFactory.Data.Contracts;
using EnsureThat;
using System.Collections.Generic;
using System.Data;

namespace Core.Business.CommandServices
{
    public class CreateCommentCommandService : ICommandService<CreateCommentCommand>
    {
        private readonly IDatabase database;

        public CreateCommentCommandService(IDatabase database)
        {
            EnsureArg.IsNotNull(database, nameof(database));

            this.database = database;
        }

        public void Execute(CreateCommentCommand command)
        {
            EnsureArg.IsNotNull(command, nameof(command));

            database.TryExecuteTransaction((transaction) =>
            {
                var dbCommand = database.CreateStoredProcCommand("Blog.CreateComment", transaction);
                var parameters = CreateParameters(command);

                foreach (var p in parameters)
                {
                    dbCommand.Parameters.Add(p);
                }

                database.Execute(dbCommand);
            });
        }

        private IEnumerable<IDataParameter> CreateParameters(CreateCommentCommand command)
        {
            yield return database.CreateParameter("UserId", command.UserID);
            yield return database.CreateParameter("ArticleId", command.ArticleID);
            yield return database.CreateParameter("Body", command.Body);

            if (command.ParentCommentID != null)
            {
                yield return database.CreateParameter("ParentCommentId", command.ParentCommentID);
            }

        }
    }
}
