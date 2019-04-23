using DatabaseFactory.Data.Contracts;
using Domain.Data.Commands.Favorite;
using EnsureThat;
using System;
using System.Collections.Generic;
using System.Data;

namespace Domain.Business.CommandServices.Favorite
{
    class CreateFavoriteArticleCommandService : ICreateFavoriteArticleCommandService
    {
        private readonly IDatabase database;

        public CreateFavoriteArticleCommandService(IDatabase database)
        {
            EnsureArg.IsNotNull(database, nameof(database));

            this.database = database;
        }

        public void Execute(CreateFavoriteArticleCommand command)
        {
            database.TryExecuteTransaction((transaction) =>
            {
                var procedure = database.CreateStoredProcCommand("Blog.CreateFavorite", transaction);

                foreach (var parameter in GetParameters(command))
                    procedure.Parameters.Add(parameter);

                database.Execute(procedure);
            });
        }

        private IEnumerable<IDataParameter> GetParameters(CreateFavoriteArticleCommand command)
        {
            yield return database.CreateParameter("@UserId", command.UserId);
            yield return database.CreateParameter("@ArticleId", command.UserId);
        }
            
    }
}
