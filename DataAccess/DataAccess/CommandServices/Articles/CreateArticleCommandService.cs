using Common;
using DatabaseFactory.Data.Contracts;
using Domain.Business.CommandServices.Articles;
using Domain.Data.Commands.Articles;
using EnsureThat;
using System.Collections.Generic;
using System.Data;

namespace DataAccess.CommandServices.Articles
{
    public class CreateArticleCommandService : ICreateArticleCommandService
    {
        private readonly IDatabase database;

        public CreateArticleCommandService(IDatabase database)
        {
            EnsureArg.IsNotNull(database, nameof(database));

            this.database = database;
        }

        public void Execute(CreateArticleCommand command)
        {
            database.TryExecuteTransaction((transaction) =>
            {
                var procedure = database.CreateStoredProcCommand("Blog.AddNewArticle", transaction);
                procedure.Parameters.AddAll(GetParameters(command));

                database.Execute(procedure);
            });
        }

        private IEnumerable<IDataParameter> GetParameters(CreateArticleCommand command)
        {
            var article = command.Article;
            yield return database.CreateParameter("@AuthorId", article.AuthorId);
            yield return database.CreateParameter("@Description", article.Description);
            yield return database.CreateParameter("@Body", article.Body);            
            yield return database.CreateParameter("@CategoryId", article.CategoryId);
        }
    }
}
