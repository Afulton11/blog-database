using Common;
using DatabaseFactory.Data.Contracts;
using Domain.Business.CommandServices.Articles;
using Domain.Data.Commands.Articles;
using Domain.Entities.Blog;
using EnsureThat;
using System.Collections.Generic;
using System.Data;

namespace DataAccess.CommandServices.Articles
{
    public class CreateOrUpdateArticleCommandService : ICreateOrUpdateArticleCommandService
    {
        private readonly IDatabase database;

        public CreateOrUpdateArticleCommandService(IDatabase database)
        {
            EnsureArg.IsNotNull(database, nameof(database));

            this.database = database;
        }

        public void Execute(CreateOrUpdateArticleCommand command)
        {
            database.TryExecuteTransaction((transaction) =>
            {
                var procedure = database.CreateStoredProcCommand("Blog.CreateOrUpdateArticle", transaction);
                procedure.Parameters.AddAll(GetParameters(command));

                database.Execute(procedure);
            });
        }

        private IEnumerable<IDataParameter> GetParameters(CreateOrUpdateArticleCommand command)
        {
            var article = command.Article;
            yield return database.CreateParameter("@ArticleId", article.ArticleId);
            yield return database.CreateParameter("@AuthorId", article.AuthorId);
            yield return database.CreateParameter("@Title", article.Title);
            yield return database.CreateParameter("@Description", article.Description);
            yield return database.CreateParameter("@Body", article.Body);
            yield return database.CreateParameter("@ContentStatus", article.ContentStatus?.Value ?? ContentStatus.Draft.Value);
            yield return database.CreateParameter("@CategoryId", article.CategoryId);
        }
    }
}
