using Domain.Business.QueryServices.Exceptions;
using Domain.Data.Queries;
using Domain.Entities.Blog;
using DatabaseFactory.Data.Contracts;
using Mocker;
using Mocker.Comparers;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using DataAccess.QueryServices;
using DataAccess.QueryServices.Readers;
using System.Linq;

namespace UnitTests.DataAccess.QueryServices.GetArticlesBetweenDatesQueryServiceTests
{

    [TestFixture]
    public class GetArticlesBetweenDatesQueryServiceNullArgumentTests
    {
        private static IEnumerable<TestCaseData> NullArgumentCases()
        {
            yield return new TestCaseData(null, null);
            yield return new TestCaseData(Mock.Of<IDatabase>(), null);
            yield return new TestCaseData(null, Mock.Of<IReader<Article>>());
        }

        [Test, TestCaseSource("NullArgumentCases")]
        public void Creation_Should_Throw_ArgumentNullException(IDatabase arg1, IReader<Article> arg2)
        {
            TestDelegate action = () => new GetArticlesBetweenDatesQueryService(arg1, arg2);

            Assert.Throws<ArgumentNullException>(action);
        }
    }


    /// <summary>
    /// Tests the <see cref="GetArticleByIdQueryService"/> class.
    /// </summary>
    public class GetArticlesBetweenDatesQueryServiceTests
    {
        protected Mock<IDatabase> databaseMock;
        protected Mock<IReader<Article>> articleReaderMock;
        protected GetArticlesBetweenDatesQueryService queryService;

         
        [SetUp]
        protected virtual void SetUp()
        {
            databaseMock = new Mock<IDatabase>();
            articleReaderMock = new Mock<IReader<Article>>();

            queryService = new GetArticlesBetweenDatesQueryService(databaseMock.Object, articleReaderMock.Object);

            SetUpMocks();
        }

        [TearDown]
        protected virtual void TearDown()
        {
        }

        protected virtual void SetUpMocks() { }

        protected string ProcedureName => "Blog.GetArticlesBetweenDates";
        protected string ParameterArticleIdName => "ArticleId";
        protected string ParameterStartDateName => "StartDate";
        protected string ParameterEndDateName => "EndDate";

        protected IEnumerable<string> AllParameters
        {
            get
            {
                yield return ParameterArticleIdName;
                yield return ParameterStartDateName;
                yield return ParameterEndDateName;
            }
        }

        protected IEqualityComparer<Article> ArticleComparer => new ArticleEqualityComparer();


    }

    [TestFixture]
    public class ExecuteShouldThrowNullArgumentException : GetArticlesBetweenDatesQueryServiceTests
    {
        private static IEnumerable<TestCaseData> NullArgumentCases()
        {
            yield return new TestCaseData(null);
        }

        [Test, TestCaseSource("NullArgumentCases")]
        public void Execution_Should_Throw_ArgumentNullException(GetArticlesBetweenDatesQuery query)
        {
            TestDelegate action = () => queryService.Execute(query);

            Assert.Throws<ArgumentNullException>(action);
        }
    }

    public class QueryExecutionTests : GetArticlesBetweenDatesQueryServiceTests
    {
        protected Mock<IDbTransaction> transactionMock;
        protected GetArticlesBetweenDatesQuery Query { get; private set; }

        protected IEnumerable<Article> ReturnValue { get; private set; }

        protected override void SetUp()
        {
            transactionMock = new Mock<IDbTransaction>();

            Query = new GetArticlesBetweenDatesQuery
            {
                ArticleID = 0,
                StartDate = new DateTimeOffset(new DateTime(2019, 1, 1)),
                EndDate = new DateTimeOffset(new DateTime(2019, 4, 23)),
            };

            base.SetUp();
        }

        protected override void SetUpMocks()
        {
            base.SetUpMocks();
            databaseMock.Setup((mock) =>
                mock.TryExecuteTransaction(It.IsAny<Func<IDbTransaction, IEnumerable<Article>>>()))
                .Returns((Func<IDbTransaction, IEnumerable<Article>> func) => func(transactionMock.Object));
        }

        protected void Execute() =>
            ReturnValue = queryService.Execute(Query);

    }

    public class StoredProcedureTests : QueryExecutionTests
    {
        protected FakeDbCommand FakeCommand { get; private set; }
        protected override void SetUpMocks()
        {
            base.SetUpMocks();

            databaseMock.Setup((mock) =>
                mock.CreateStoredProcCommand(
                    It.Is<string>((val) => val == ProcedureName),
                    It.Is<IDbTransaction>((val) => val == transactionMock.Object)))
                .Returns(FakeCommand = new FakeDbCommand
                {
                    CommandText = ProcedureName,
                    Transaction = transactionMock.Object
                });
        }

    }

    public class CommandParameterTests : StoredProcedureTests
    {
        protected FakeDbParameter FakeArticleIdParameter { get; private set; }
        protected FakeDbParameter FakeStartDateParameter { get; private set; }
        protected FakeDbParameter FakeEndDateParameter { get; private set; }
        protected override void SetUpMocks()
        {
            base.SetUpMocks();

            databaseMock.Setup((mock) =>
                mock.CreateParameter(
                    It.Is<string>((val) => val == ParameterArticleIdName),
                    It.Is<int>((val) => val == Query.ArticleID)))
                .Returns(FakeArticleIdParameter = new FakeDbParameter
                {
                    ParameterName = ParameterArticleIdName,
                    Value = Query.ArticleID,
                });

            databaseMock.Setup((mock) =>
                mock.CreateParameter(
                    It.Is<string>((val) => val == ParameterStartDateName),
                    It.Is<DateTimeOffset>((val) => val == Query.StartDate)))
                .Returns(FakeStartDateParameter = new FakeDbParameter
                {
                    ParameterName = ParameterStartDateName,
                    Value = Query.ArticleID,
                });

            databaseMock.Setup((mock) =>
                mock.CreateParameter(
                    It.Is<string>((val) => val == ParameterEndDateName),
                    It.Is<DateTimeOffset>((val) => val == Query.EndDate)))
                .Returns(FakeEndDateParameter = new FakeDbParameter
                {
                    ParameterName = ParameterEndDateName,
                    Value = Query.ArticleID,
                });
        }
    }

    public abstract class DataReaderTests : CommandParameterTests
    {
        protected Mock<IDataReader> DataReaderMock;

        protected abstract IList<Article> ReadArticles();

        protected override void SetUp()
        {
            DataReaderMock = new Mock<IDataReader>();
            base.SetUp();
        }

        protected override void SetUpMocks()
        {
            base.SetUpMocks();

            databaseMock.Setup((mock) =>
                mock.ExecuteReader(It.IsAny<IDbCommand>(), It.IsAny<Func<IDataReader, IEnumerable<Article>>>()))
                .Returns((IDbCommand command, Func<IDataReader, IEnumerable<Article>>  func) =>
                    func(DataReaderMock.Object));

            articleReaderMock.Setup((mock) =>
                mock.Read(DataReaderMock.Object))
               .Returns(ReadArticles());
        }
    }

    public class SuccessfulReaderTests : DataReaderTests
    {
        protected override IList<Article> ReadArticles()
        {
            var article = new Article
            {
                ArticleId = Query.ArticleID,
                AuthorId = 1,
                Body = "Body",
                CategoryId = 0,
                ContentStatus = ContentStatus.Published,
                CreationDateTime = new DateTime(2017, 2, 10),
                LastUpdateDateTime = new DateTime(2019, 4, 13),
                Description = "Description",
                Title = "Title",
            };

            var list = new List<Article>();
            list.Add(article);

            return list;
        }

        [Test]
        public void Should_Create_One_Stored_Procedure_With_Transaction()
        {
            Execute();

            databaseMock.Verify((mock) =>
                mock.CreateStoredProcCommand(
                    It.Is<string>((val) => val == ProcedureName),
                    It.Is<IDbTransaction>((val) => val == transactionMock.Object)),
                Times.Once());
        }

        [Test]
        public void Should_Create_ArticleId_Parameter()
        {
            Execute();

            databaseMock.Verify((mock) =>
                mock.CreateParameter(
                    It.Is<string>((val) => val == ParameterArticleIdName),
                    It.Is<int>((val) => val == Query.ArticleID)),
                Times.Once());
        }

        [Test]
        public void Should_Create_StartDate_Parameter()
        {
            Execute();

            databaseMock.Verify((mock) =>
                mock.CreateParameter(
                    It.Is<string>((val) => val == ParameterStartDateName),
                    It.Is<DateTimeOffset>((val) => val == Query.StartDate)),
                Times.Once());
        }

        [Test]
        public void Should_Create_EndDate_Parameter()
        {
            Execute();

            databaseMock.Verify((mock) =>
                mock.CreateParameter(
                    It.Is<string>((val) => val == ParameterStartDateName),
                    It.Is<DateTimeOffset>((val) => val == Query.StartDate)),
                Times.Once());
        }


        [Test]
        public void Should_Execute_Reader_Using_Parameter_Once()
        {
            Execute();

            Predicate<FakeDbCommand> isCorrectCommand = (val) =>
                 val == FakeCommand
                 && val.ActualParameters.Count == 3
                 && val.ActualParameters.Contains(FakeArticleIdParameter)
                 && val.ActualParameters.Contains(FakeStartDateParameter)
                 && val.ActualParameters.Contains(FakeEndDateParameter);

            databaseMock.Verify((mock) =>
                mock.ExecuteReader(
                    It.Is<FakeDbCommand>((val) => isCorrectCommand(val)),
                    It.IsAny<Func<IDataReader, IEnumerable<Article>>>()),
                Times.Once());
        }

        [Test]
        public void Should_Return_Same_List_Of_Articles()
        {
            Execute();

            Assert.That(ReturnValue,
                Is.EqualTo(ReadArticles())
                  .Using(ArticleComparer));
        }

    }

    public class FailedArticlesTest : DataReaderTests
    {
        protected override IList<Article> ReadArticles() => null;

        [Test]
        public void Should_Throw_ArticlesNotFoundException()
        {
            Assert.Throws<ArticlesNotFoundException>(Execute);
        }
    }

}
