using System.Collections.Generic;
using System.Data;
using Common;
using DatabaseFactory.Data.Contracts;
using Domain.Business.CommandServices.Articles;
using Domain.Data.Commands.Articles;
using EnsureThat;

namespace DataAccess.CommandServices.Articles
{
    public class UpdateArticleCommandService : IUpdateArticleCommandService
    {
        private IDatabase database;

        public UpdateArticleCommandService(IDatabase database)
        {
            EnsureArg.IsNotNull(database, nameof(database));

            this.database = database;
        }

        public void Execute(UpdateArticleCommand command)
        {
            database.TryExecuteTransaction((transaction) =>
            {
                var procedure = database.CreateStoredProcCommand("Blog.UpdateArticle", transaction);
                procedure.Parameters.AddAll(GetParameters(command));

                database.Execute(procedure);
            });
        }

        private IEnumerable<IDataParameter> GetParameters(UpdateArticleCommand command)
        {
            var article = command.Article;
            yield return database.CreateParameter("@ArticleId", article.ArticleId);
            yield return database.CreateParameter("@Title", article.Title);
            yield return database.CreateParameter("@Descriptoion", article.Description);
            yield return database.CreateParameter("@Body", article.Body);
            yield return database.CreateParameter("@CategoryId", article.CategoryId);
        }
    }
}
