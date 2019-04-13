using Core.Business.Contracts;
using Core.Business.QueryServices.Readers;
using Core.Data.Queries;
using Core.Entities.Blog;
using DatabaseFactory.Data.Contracts;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Core.Business.QueryServices
{
    /// <summary>
    /// Retrieves an article using the given <see cref="GetArticleByIdQuery"/> if it exists in the <see cref="IDatabase"/>.
    /// </summary>
    public class GetArticleByIdQueryService : IQueryService<GetArticleByIdQuery, Article>
    {
        public GetArticleByIdQueryService(IDatabase database, IReader<Article> articleReader)
        {
            Database = database;
            ArticleReader = articleReader;
        }
        public IDatabase Database { get; }
        public IReader<Article> ArticleReader { get; }

        public Article Execute(GetArticleByIdQuery query)
        {
            return Database.TryExecuteTransaction((transaction) =>
            {
                var dbQuery = Database.CreateStoredProcCommand("Blog.GetArticleById", transaction);

                dbQuery.Parameters.Add(Database.CreateParameter("ArticleId", query.ArticleID));

                return Database.ExecuteReader(dbQuery, (reader) => this.ReadArticles(reader, query));
            });
        }

        private Article ReadArticles(IDataReader reader, GetArticleByIdQuery query)
        {
            var article = ArticleReader.Read(reader).FirstOrDefault();

            if (article == null)
            {
                throw new KeyNotFoundException($"No Article with Id[{query.ArticleID}] was found in the database!");
            }

            return article;
        }
    }
}
