using System;
using System.Data;
using System.Reflection;
using DatabaseFactory.Config;
using DatabaseFactory.Data.Contracts;
using DatabaseFactory.Data.Exceptions;
using EnsureThat;

namespace DatabaseFactory.Data
{
    public abstract class Database : IDatabase
    {
        protected readonly DatabaseOptions options;

        protected Database()
            : this(new DatabaseOptions<Database>())
        {
        }

        protected Database(DatabaseOptions options)
        {
            EnsureArg.IsNotNull(options, nameof(options));

            if (!options.ContextType.GetTypeInfo().IsAssignableFrom(GetType().GetTypeInfo()))
            {
                throw new InvalidOperationException(
$@"parameter {nameof(options)} must be assignable from {GetType().GetTypeInfo().FullName}!"
                    );
            }

            this.options = options;
        }

        public abstract IDbConnection CreateConnection();
        public abstract IDbCommand CreateCommand();
        public abstract IDbConnection CreateOpenConnection();
        public abstract IDbCommand CreateCommand(string commandText, IDbConnection connection);
        public abstract IDbCommand CreateCommand(string commandText, IDbTransaction transaction);
        public abstract IDbCommand CreateStoredProcCommand(string procName, IDbConnection connection);
        public abstract IDbCommand CreateStoredProcCommand(string procName, IDbTransaction transaction);
        public abstract IDataParameter CreateParameter(string parameterName, object parameterValue);

        /// <summary>
        /// Creates a connection the database, then begins a new transaction.
        /// </summary>
        /// <param name="transactionAction">The function to run before executing the transaction</param>
        /// <exception cref="Exception">The transaction failed, but was successfully rolled back.</exception>
        /// <exception cref=""></exception>
        public void TryExecuteTransaction(Action<IDbTransaction> transactionAction)
        {
            using (IDbConnection connection = CreateOpenConnection())
            {
                var transaction = connection.BeginTransaction();

                try
                {
                    transactionAction(transaction);

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    TryRollback(transaction);
                    throw ex;
                }
            }
        }

        public void TryRollback(IDbTransaction transaction)
        {
            try
            {
                transaction.Rollback();
            }
            catch (Exception ex)
            {
                throw new RollbackFailedException(transaction, ex);
            }
        }

        public void ExecuteReader(IDbCommand command, Action<IDataReader> onRead)
        {
            using (command)
            {
                using (IDataReader reader = command.ExecuteReader())
                {
                    onRead?.Invoke(reader);
                }
            }
        }

        public T ExecuteScalar<T>(IDbCommand command)
        {
            using (command)
            {
                return (T)command.ExecuteScalar();
            }
        }

        public int Execute(IDbCommand command)
        {
            using (command)
            {
                return command.ExecuteNonQuery();
            }
        }

    }
}
