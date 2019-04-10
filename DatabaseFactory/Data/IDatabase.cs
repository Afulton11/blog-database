using System.Data;

namespace DatabaseFactory.Data.Contracts
{
    public interface IDatabase
    {
       IDbConnection CreateConnection();
       IDbCommand CreateCommand();
       IDbConnection CreateOpenConnection();
       IDbCommand CreateCommand(string commandText, IDbConnection connection);
       IDbCommand CreateStoredProcCommand(string procName, IDbConnection connection);
       IDataParameter CreateParameter(string parameterName, object parameterValue);
    }
}
