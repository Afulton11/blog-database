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
using System.Linq;
using DataAccess.QueryServices;
using DataAccess.QueryServices.Readers;

namespace UnitTests.DataAccess.QueryServices.GetArticleByIdQueryServiceTests
{

    [TestFixture]
    public class GetArticleByIdQueryServiceNullArgumentTests
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
            TestDelegate action = () => new GetArticleByIdQueryService(arg1, arg2);

            Assert.Throws<ArgumentNullException>(action);
        }
    }


    /// <summary>
    /// Tests the <see cref="GetArticleByIdQueryService"/> class.
    /// </summary>
    public class GetArticleByIdQueryServiceTests
    {
        protected Mock<IDatabase> databaseMock;
        protected Mock<IReader<Article>> articleReaderMock;
        protected GetArticleByIdQueryService queryService;
        protected string ProcedureName => "Blog.GetArticleById";
        protected string ParameterName => "@ArticleId";

        protected IEqualityComparer<Article> ArticleComparer => new ArticleEqualityComparer();
         
        [SetUp]
        protected virtual void SetUp()
        {
            databaseMock = new Mock<IDatabase>();
            articleReaderMock = new Mock<IReader<Article>>();

            queryService = new GetArticleByIdQueryService(databaseMock.Object, articleReaderMock.Object);

            SetUpMocks();
        }

        [TearDown]
        protected virtual void TearDown()
        {
        }

        protected virtual void SetUpMocks() { }

    }

    [TestFixture]
    public class ExecuteShouldThrowNullArgumentException : GetArticleByIdQueryServiceTests
    {
        private static IEnumerable<TestCaseData> NullArgumentCases()
        {
            yield return new TestCaseData(null);
        }

        [Test, TestCaseSource("NullArgumentCases")]
        public void Execution_Should_Throw_ArgumentNullException(GetArticleByIdQuery query)
        {
            TestDelegate action = () => queryService.Execute(query);

            Assert.Throws<ArgumentNullException>(action);
        }
    }

    public class QueryExecutionTests : GetArticleByIdQueryServiceTests
    {
        protected Mock<IDbTransaction> transactionMock;
        protected GetArticleByIdQuery Query { get; private set; }

        protected Article ReturnValue { get; private set; }

        protected override void SetUp()
        {
            transactionMock = new Mock<IDbTransaction>();

            Query = new GetArticleByIdQuery
            {
                ArticleID = 0
            };

            base.SetUp();
        }

        protected override void SetUpMocks()
        {
            base.SetUpMocks();
            databaseMock.Setup((mock) =>
                mock.TryExecuteTransaction(It.IsAny<Func<IDbTransaction, Article>>()))
                .Returns((Func<IDbTransaction, Article> func) => func(transactionMock.Object));
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
                mock.CreateStoredProcCommand(It.Is<string>((val) => val == ProcedureName), It.Is<IDbTransaction>((val) => val == transactionMock.Object)))
                .Returns(FakeCommand = new FakeDbCommand
                {
                    CommandText = ProcedureName,
                    Transaction = transactionMock.Object
                });
        }

    }

    public class CommandParameterTests : StoredProcedureTests
    {
        protected FakeDbParameter FakeParameter { get; private set; }
        protected override void SetUpMocks()
        {
            base.SetUpMocks();

            databaseMock.Setup((mock) =>
                mock.CreateParameter(It.Is<string>((val) => val == ParameterName), It.Is<int>((val) => val == Query.ArticleID)))
                .Returns(FakeParameter = new FakeDbParameter
                {
                    ParameterName = ParameterName,
                    Value = Query.ArticleID,
                });
        }
    }

    public abstract class DataReaderTests : CommandParameterTests
    {
        protected Mock<IDataReader> DataReaderMock;

        protected abstract IEnumerable<Article> ReadArticles();

        protected override void SetUp()
        {
            DataReaderMock = new Mock<IDataReader>();
            base.SetUp();
        }

        protected override void SetUpMocks()
        {
            base.SetUpMocks();

            databaseMock.Setup((mock) =>
                mock.ExecuteReader(It.IsAny<IDbCommand>(), It.IsAny<Func<IDataReader, Article>>()))
                .Returns((IDbCommand command, Func<IDataReader, Article>  func) =>
                    func(DataReaderMock.Object));

            articleReaderMock.Setup((mock) =>
                mock.Read(DataReaderMock.Object))
               .Returns(ReadArticles());
        }
    }

    public class SuccessfulReaderTests : DataReaderTests
    {
        protected override IEnumerable<Article> ReadArticles()
        {
            yield return new Article
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
        }

        [Test]
        public void Should_Create_One_Stored_Procedure_With_Transaction()
        {
            Execute();

            databaseMock.Verify((mock) =>
                mock.CreateStoredProcCommand(It.Is<string>((val) => val == ProcedureName), It.Is<IDbTransaction>((val) => val == transactionMock.Object)),
                Times.Once());
        }

        [Test]
        public void Should_Create_One_Parameter()
        {
            Execute();

            databaseMock.Verify((mock) =>
                mock.CreateParameter(It.IsAny<string>(), It.IsAny<object>()),
                Times.Once());
        }

        [Test]
        public void Should_Execute_Reader_Using_Parameter_Once()
        {
            Execute();

            Predicate<FakeDbCommand> isCorrectCommand = (val) =>
                 val == FakeCommand
                 && val.ActualParameters.Count == 1
                 && val.ActualParameters.Contains(FakeParameter);

            databaseMock.Verify((mock) =>
                mock.ExecuteReader(It.Is<FakeDbCommand>((val) => isCorrectCommand(val)), It.IsAny<Func<IDataReader, Article>>()),
                Times.Once());
        }

        [Test]
        public void Should_Return_Same_Article()
        {
            Execute();

            Assert.That(ReturnValue,
                Is.EqualTo(ReadArticles().First())
                  .Using(ArticleComparer));
        }

    }

    public class FailedReaderTests : DataReaderTests
    {
        protected override IEnumerable<Article> ReadArticles() => null;

        [Test]
        public void Should_Throw_KeyNotFoundException()
        {
            Assert.Throws<KeyNotFoundException>(Execute);
        }
    }

}
