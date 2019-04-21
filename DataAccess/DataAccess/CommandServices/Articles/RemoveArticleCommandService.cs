using System.Collections.Generic;
using System.Data;
using DatabaseFactory.Data.Contracts;
using Domain.Business.CommandServices.Articles;
using Domain.Data.Commands.Articles;

namespace DataAccess.CommandServices.Articles
{
    public class RemoveArticleCommandService :
        DbCommandService<RemoveArticleCommand>,
        IRemoveArticleCommandService
    {
        public RemoveArticleCommandService(IDatabase database) : base(database)
        {
        }

        protected override string ProcedureName => "Blog.RemoveArticle";
        protected override IEnumerable<IDataParameter> GetParameters(RemoveArticleCommand command)
        {
            yield return Database.CreateParameter("@ArticleId", command.ArticleId);
        }
    }
}
