using DataAccess.CommandServices;
using DatabaseFactory.Data.Contracts;
using Domain.Business.CommandServices.ArticleCategoreis;
using Domain.Data.Commands.ArticleCategories;
using System.Collections.Generic;
using System.Data;

namespace DataAccess.DataAccess.CommandServices.ArticleCategories
{
    public class UpdateArticleCategoryCommandService : 
        DbCommandService<UpdateArticleCategoryCommand>,
        IUpdateArticleCategoryCommandService
    {
        public UpdateArticleCategoryCommandService(IDatabase database) : base(database)
        {
        }

        protected override IEnumerable<IDataParameter> GetParameters(UpdateArticleCategoryCommand command)
        {
            var articleCategory = command.ArticleCategory;
            yield return Database.CreateParameter("@ArticleCategoryId", articleCategory.ArticleCategoryID);
            yield return Database.CreateParameter("@Name", articleCategory.Name);
            yield return Database.CreateParameter("@CreationUserId", articleCategory.CreationUserID);
        }

        protected override string ProcedureName => "Blog.UpdateArticleCategory";
    }
}
