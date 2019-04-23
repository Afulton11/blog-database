using Common;
using DatabaseFactory.Data.Contracts;
using Domain.Business.QueryServices;
using Domain.Data.Queries;
using EnsureThat;
using System.Collections.Generic;
using System.Data;

namespace DataAccess.QueryServices
{
    public abstract class DbQueryService<TQuery, TResult> : IQueryService<TQuery, TResult>
        where TQuery : IQuery<TResult>
    {
        public DbQueryService(IDatabase database)
        {
            EnsureArg.IsNotNull(database, nameof(database));

            Database = database;
        }

        public virtual TResult Execute(TQuery query)
        {
            return Database.TryExecuteTransaction((transaction) =>
            {
                var procedure = Database.CreateStoredProcCommand(ProcedureName, transaction);
                procedure.Parameters.AddAll(GetParameters(query));

                return Database.ExecuteReader(procedure, (reader) => this.ReadQueryResult(reader, query));
            });
        }

        protected abstract TResult ReadQueryResult(IDataReader reader, TQuery query);
        protected abstract IEnumerable<IDataParameter> GetParameters(TQuery query);
        protected IDatabase Database { get; }
        protected abstract string ProcedureName { get; }
    }
}
