using Domain.Entities.Blog;
using DatabaseFactory.Data.Contracts;
using Mocker;
using Mocker.Comparers;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using Domain.Data.Commands.Comments;
using DataAccess.CommandServices.Comments;

namespace UnitTests.DataAccess.CommandServices.CreateCommentCommandServiceTests
{

    [TestFixture]
    public class CreateCommentCommandServiceNullArgumentTests
    {
        private static IEnumerable<TestCaseData> NullArgumentCases()
        {
            yield return new TestCaseData(null);
        }

        [Test, TestCaseSource("NullArgumentCases")]
        public void Creation_Should_Throw_ArgumentNullException(IDatabase arg1)
        {
            TestDelegate action = () => new CreateCommentCommandService(arg1);

            Assert.Throws<ArgumentNullException>(action);
        }
    }


    /// <summary>
    /// Tests the <see cref="GetArticleByIdQueryService"/> class.
    /// </summary>
    public class CreateCommentCommandServiceTests
    {
        protected Mock<IDatabase> databaseMock;
        protected CreateCommentCommandService cmdService;


        [SetUp]
        protected virtual void SetUp()
        {
            databaseMock = new Mock<IDatabase>();

            cmdService = new CreateCommentCommandService(databaseMock.Object);

            SetUpMocks();
        }

        [TearDown]
        protected virtual void TearDown()
        {
        }

        protected virtual void SetUpMocks() { }

        protected string ProcedureName => "Blog.CreateComment";
        protected string ParameterUserId => "UserId";
        protected string ParameterArticleId => "ArticleId";
        protected string ParameterBody => "Body";
        protected string ParameterParentCommentId => "ParentCommentId";

        protected IEnumerable<string> AllParameters
        {
            get
            {
                yield return ParameterArticleId;
                yield return ParameterBody;
                yield return ParameterUserId;
                yield return ParameterParentCommentId;
            }
        }

        protected IEqualityComparer<Article> ArticleComparer => new ArticleEqualityComparer();


    }

    [TestFixture]
    public class ExecuteShouldThrowNullArgumentException : CreateCommentCommandServiceTests
    {
        private static IEnumerable<TestCaseData> NullArgumentCases()
        {
            yield return new TestCaseData(null);
        }

        [Test, TestCaseSource("NullArgumentCases")]
        public void Execution_Should_Throw_ArgumentNullException(CreateCommentCommand cmd)
        {
            TestDelegate action = () => cmdService.Execute(cmd);

            Assert.Throws<ArgumentNullException>(action);
        }
    }

    public class CommandExecutionTests : CreateCommentCommandServiceTests
    {
        protected Mock<IDbTransaction> transactionMock;

        protected virtual CreateCommentCommand Command => new CreateCommentCommand
        {
            UserID = 0,
            ArticleID = 1,
            Body = "Hello there",
            ParentCommentID = 1,
        };

        protected override void SetUp()
        {
            transactionMock = new Mock<IDbTransaction>();

            base.SetUp();
        }

        protected override void SetUpMocks()
        {
            base.SetUpMocks();
            databaseMock.Setup((mock) =>
                mock.TryExecuteTransaction(It.IsAny<Action<IDbTransaction>>()))
                .Callback((Action<IDbTransaction> func) => func(transactionMock.Object));
        }

        protected void Execute() =>
            cmdService.Execute(Command);

    }

    public class StoredProcedureTests : CommandExecutionTests
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
        protected FakeDbParameter FakeUserIdParameter { get; private set; }
        protected FakeDbParameter FakeArticleIdParameter { get; private set; }
        protected FakeDbParameter FakeBodyParameter { get; private set; }
        protected FakeDbParameter FakeParentCommentIdParameter { get; private set; }
        protected override void SetUpMocks()
        {
            base.SetUpMocks();

            databaseMock.Setup((mock) =>
                mock.CreateParameter(
                    It.Is<string>((val) => val == ParameterUserId),
                    It.IsAny<int>()))
                .Returns((string name, int value) =>
                    FakeUserIdParameter = new FakeDbParameter
                    {
                        ParameterName = name,
                        Value = value
                    });

            databaseMock.Setup((mock) =>
                mock.CreateParameter(
                    It.Is<string>((val) => val == ParameterArticleId),
                    It.IsAny<int>()))
                .Returns((string name, int value) =>
                    FakeArticleIdParameter = new FakeDbParameter
                    {
                        ParameterName = name,
                        Value = value
                    });

            databaseMock.Setup((mock) =>
                mock.CreateParameter(
                    It.Is<string>((val) => val == ParameterBody),
                    It.IsAny<string>()))
                .Returns((string name, string value) =>
                    FakeBodyParameter = new FakeDbParameter
                    {
                        ParameterName = name,
                        Value = value
                    });

            databaseMock.Setup((mock) =>
                mock.CreateParameter(
                    It.Is<string>((val) => val == ParameterParentCommentId),
                    It.IsAny<int>()))
                .Returns((string name, int value) =>
                    FakeParentCommentIdParameter = new FakeDbParameter
                    {
                        ParameterName = name,
                        Value = value
                    });
        }
    }

    public class ExecuteWithParentCommentIdTests : CommandParameterTests
    {

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
        public void Should_Create_UserId_Parameter()
        {
            Execute();

            databaseMock.Verify((mock) =>
                mock.CreateParameter(
                    It.Is<string>((val) => val == ParameterUserId),
                    It.Is<int>((val) => val == Command.UserID)),
                Times.Once());
        }

        [Test]
        public void Should_Create_ArticleId_Parameter()
        {
            Execute();

            databaseMock.Verify((mock) =>
                mock.CreateParameter(
                    It.Is<string>((val) => val == ParameterArticleId),
                    It.Is<int>((val) => val == Command.ArticleID)),
                Times.Once());
        }

        [Test]
        public void Should_Create_Body_Parameter()
        {
            Execute();

            databaseMock.Verify((mock) =>
                mock.CreateParameter(
                    It.Is<string>((val) => val == ParameterBody),
                    It.Is<string>((val) => val == Command.Body)),
                Times.Once());
        }

        [Test]
        public void Should_Create_ParentCommentId_Parameter()
        {
            Execute();

            databaseMock.Verify((mock) =>
                mock.CreateParameter(
                    It.Is<string>((val) => val == ParameterParentCommentId),
                    It.Is<int>((val) => val == Command.ParentCommentID)),
                Times.Once());
        }

        [Test]
        public void Should_Execute_Using_Parameters_Once()
        {
            Execute();

            Predicate<FakeDbCommand> isCorrectCommand = (val) =>
                 val == FakeCommand
                 && val.ActualParameters.Count == 4
                 && val.ActualParameters.Contains(FakeUserIdParameter)
                 && val.ActualParameters.Contains(FakeArticleIdParameter)
                 && val.ActualParameters.Contains(FakeBodyParameter)
                 && val.ActualParameters.Contains(FakeParentCommentIdParameter);

            databaseMock.Verify((mock) =>
                mock.Execute(It.Is<FakeDbCommand>((val) => isCorrectCommand(val))),
                Times.Once());
        }
    }

    public class ExecuteWithNullParentCommentIdTests : CommandParameterTests
    {
        protected override CreateCommentCommand Command => new CreateCommentCommand
        {
            UserID = 0,
            ArticleID = 1,
            Body = "Hello there",
        };

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
        public void Should_Create_UserId_Parameter()
        {
            Execute();

            databaseMock.Verify((mock) =>
                mock.CreateParameter(
                    It.Is<string>((val) => val == ParameterUserId),
                    It.Is<int>((val) => val == Command.UserID)),
                Times.Once());
        }

        [Test]
        public void Should_Create_ArticleId_Parameter()
        {
            Execute();

            databaseMock.Verify((mock) =>
                mock.CreateParameter(
                    It.Is<string>((val) => val == ParameterArticleId),
                    It.Is<int>((val) => val == Command.ArticleID)),
                Times.Once());
        }

        [Test]
        public void Should_Create_Body_Parameter()
        {
            Execute();

            databaseMock.Verify((mock) =>
                mock.CreateParameter(
                    It.Is<string>((val) => val == ParameterBody),
                    It.Is<string>((val) => val == Command.Body)),
                Times.Once());
        }

        [Test]
        public void Should_Not_Create_ParentCommentId_Parameter()
        {
            Execute();

            databaseMock.Verify((mock) =>
                mock.CreateParameter(
                    It.Is<string>((val) => val == ParameterParentCommentId),
                    It.Is<int>((val) => val == Command.ParentCommentID)),
                Times.Never());
        }


        [Test]
        public void Should_Execute_Using_Parameters_Once()
        {
            Execute();

            Predicate<FakeDbCommand> isCorrectCommand = (val) =>
                 val == FakeCommand
                 && val.ActualParameters.Count == 3
                 && val.ActualParameters.Contains(FakeUserIdParameter)
                 && val.ActualParameters.Contains(FakeArticleIdParameter)
                 && val.ActualParameters.Contains(FakeBodyParameter)
                 && !val.ActualParameters.Contains(FakeParentCommentIdParameter);

            databaseMock.Verify((mock) =>
                mock.Execute(It.Is<FakeDbCommand>((val) => isCorrectCommand(val))),
                Times.Once());
        }
    }
}
