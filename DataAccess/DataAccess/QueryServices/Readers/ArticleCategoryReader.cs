using Common;
using DataAccess.QueryServices.Readers;
using Domain.Entities.Blog;
using System;
using System.Data;

namespace DataAccess.DataAccess.QueryServices.Readers
{
    public class ArticleCategoryReader : Reader<ArticleCategory>
    {
        protected override ArticleCategory ReadRow(IDataRecord row) =>
            new ArticleCategory
            {
                ArticleCategoryID = row.GetSafely<int>("ArticleCategoryId"),
                Name = row.GetSafely<string>("Name"),
                CreationDateTime = row.GetSafely<DateTime>("CreationDateTime"),
                CreationUserID = row.GetSafely<int>("CreationUserId"),
                LastUpdateDateTime = row.GetSafely<DateTime>("LastUpdateDateTime"),
            };
    }
}
