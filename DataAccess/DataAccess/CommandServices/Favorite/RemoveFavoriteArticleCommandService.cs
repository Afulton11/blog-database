using DatabaseFactory.Data.Contracts;
using EnsureThat;
using System.Collections.Generic;
using System.Data;

namespace Domain.Business.CommandServices.Favorite
{
    class RemoveFavoriteArticleCommandService : IRemoveFavoriteArticleCommandService
    {
        private readonly IDatabase database;

        public RemoveFavoriteArticleCommandService(IDatabase database)
        {
            EnsureArg.IsNotNull(database, nameof(database));

            this.database = database;
        }

        public void Execute(RemoveFavoriteArticleCommand command)
        {
            database.TryExecuteTransaction((transaction) =>
            {
                var procedure = database.CreateStoredProcCommand("Blog.RemoveFavorite", transaction);

                foreach (var parameter in GetParameters(command))
                    procedure.Parameters.Add(parameter);

                database.Execute(procedure);
            });
        }

        private IEnumerable<IDataParameter> GetParameters(RemoveFavoriteArticleCommand command)
        {
            yield return database.CreateParameter("@UserId", command.UserId);
            yield return database.CreateParameter("@ArticleId", command.UserId);
        }
            
    }
}
