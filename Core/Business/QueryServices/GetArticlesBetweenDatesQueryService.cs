using Core.Business.Contracts;
using Core.Business.QueryServices.Exceptions;
using Core.Business.QueryServices.Readers;
using Core.Data.Queries;
using Core.Entities.Blog;
using DatabaseFactory.Data.Contracts;
using EnsureThat;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Core.Business.QueryServices
{
    /// <summary>
    /// Retrieves a list of articles using the given <see cref="GetArticlesBetweenDatesQuery"/>
    /// </summary>
    public class GetArticlesBetweenDatesQueryService : IQueryService<GetArticlesBetweenDatesQuery, IEnumerable<Article>>
    {
        public GetArticlesBetweenDatesQueryService(IDatabase database, IReader<Article> articleReader)
        {
            EnsureArg.IsNotNull(database, nameof(database));
            EnsureArg.IsNotNull(articleReader, nameof(articleReader));

            Database = database;
            ArticleReader = articleReader;
        }
        public IDatabase Database { get; }
        public IReader<Article> ArticleReader { get; }

        public IEnumerable<Article> Execute(GetArticlesBetweenDatesQuery query)
        {
            EnsureArg.IsNotNull(query, nameof(query));

            return Database.TryExecuteTransaction((transaction) =>
            {
                var dbQuery = Database.CreateStoredProcCommand("Blog.GetArticlesBetweenDates", transaction);
                var articleId = Database.CreateParameter("ArticleId", query.ArticleID);
                var startDate = Database.CreateParameter("StartDate", query.StartDate);
                var endDate = Database.CreateParameter("EndDate", query.EndDate);

                dbQuery.Parameters.Add(articleId);
                dbQuery.Parameters.Add(startDate);
                dbQuery.Parameters.Add(endDate);

                return Database.ExecuteReader(dbQuery, (reader) => this.ReadArticles(reader, query));
            });
        }

        private IEnumerable<Article> ReadArticles(IDataReader reader, GetArticlesBetweenDatesQuery query)
        {
            var articles = ArticleReader.Read(reader);

            if (articles == null || !articles.Any())
            {
                throw new ArticlesNotFoundException(query.StartDate, query.EndDate);
            }

            return articles;
        }
    }
}
