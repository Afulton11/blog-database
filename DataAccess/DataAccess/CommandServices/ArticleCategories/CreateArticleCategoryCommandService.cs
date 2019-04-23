using DataAccess.CommandServices;
using DatabaseFactory.Data.Contracts;
using Domain.Business.CommandServices.ArticleCategoreis;
using Domain.Data.Commands.ArticleCategories;
using System;
using System.Collections.Generic;
using System.Data;

namespace DataAccess.DataAccess.CommandServices.ArticleCategories
{
    public class CreateArticleCategoryCommandService : 
        DbCommandService<CreateArticleCategoryCommand>,
        ICreateArticleCategoryCommandService
    {
        public CreateArticleCategoryCommandService(IDatabase database) : base(database)
        {
        }

        protected override IEnumerable<IDataParameter> GetParameters(CreateArticleCategoryCommand command)
        {
            yield return Database.CreateParameter("@Name", command.Name);
            yield return Database.CreateParameter("@CreationUserId", command.CreationUserId);
        }

        protected override string ProcedureName => "Blog.CreateArticleCategory";
    }
}
