using System;
using System.Collections.Generic;
using System.Data;

namespace Mocker
{
    public class FakeDbCommand : IDbCommand
    {
        private FakeDbDataParameterCollection mockedParameters;
        public FakeDbCommand()
        {
            mockedParameters = new FakeDbDataParameterCollection();
        }

        public void Cancel()
        {
            throw new NotImplementedException();
        }

        public IDbDataParameter CreateParameter()
        {
            throw new NotImplementedException();
        }

        public int ExecuteNonQuery()
        {
            throw new NotImplementedException();
        }

        public IDataReader ExecuteReader()
        {
            throw new NotImplementedException();
        }

        public IDataReader ExecuteReader(CommandBehavior behavior)
        {
            throw new NotImplementedException();
        }

        public object ExecuteScalar()
        {
            throw new NotImplementedException();
        }

        public void Prepare()
        {
            throw new NotImplementedException();
        }

        public string CommandText { get; set; }
        public int CommandTimeout { get; set; }
        public CommandType CommandType { get; set; }
        public IDbConnection Connection { get; set; }
        public IDataParameterCollection Parameters => mockedParameters;
        public IList<object> ActualParameters => mockedParameters.ActualParameters;
        public IDbTransaction Transaction { get; set; }
        public UpdateRowSource UpdatedRowSource { get; set; }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public override bool Equals(object obj)
        {
            return obj is FakeDbCommand command &&
                   CommandText == command.CommandText &&
                   EqualityComparer<IDbTransaction>.Default.Equals(Transaction, command.Transaction);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(CommandText, Transaction);
        }
    }
}
