using DatabaseFactory.Data.Contracts;
using Moq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;

namespace Mocker
{
    /// <summary>
    /// The Fake database class to use when creating tests for a QueryService.
    /// </summary>
    public class FakeQueryDatabase : IDatabase
    {
        public IDbTransaction Transaction { get; }

        public FakeQueryDatabase()
        {
            Transaction = Mock.Of<IDbTransaction>();
        }

        public IDbConnection CreateConnection() => null;

        public IDbCommand CreateCommand() => null;
        public IDbConnection CreateOpenConnection() => null;

        public IDbCommand CreateCommand(string commandText, IDbConnection connection) => null;

        public IDbCommand CreateStoredProcCommand(string procName, IDbConnection connection) => null;

        public IDbCommand CreateStoredProcCommand(string procName, IDbTransaction transaction)
        {
            return new FakeDbCommand()
            {
                CommandText = procName,
                Transaction = transaction
            };
        }


        public IDataParameter CreateParameter(string parameterName, object parameterValue)
        {
            throw new NotImplementedException();
        }

        public void TryExecuteTransaction(Action<IDbTransaction> transactionAction)
        {
            throw new NotImplementedException();
        }

        public TResult TryExecuteTransaction<TResult>(Func<IDbTransaction, TResult> transactionFunc)
        {
            return transactionFunc(Transaction);
        }

        public void TryRollback(IDbTransaction transaction)
        {
            throw new NotImplementedException();
        }

        public void ExecuteReader(IDbCommand command, Action<IDataReader> onRead)
        {
            throw new NotImplementedException();
        }

        public TResult ExecuteReader<TResult>(IDbCommand command, Func<IDataReader, TResult> onRead)
        {
            throw new NotImplementedException();
        }

        public T ExecuteScalar<T>(IDbCommand command)
        {
            throw new NotImplementedException();
        }

        public int Execute(IDbCommand command)
        {
            throw new NotImplementedException();
        }
    }
}
