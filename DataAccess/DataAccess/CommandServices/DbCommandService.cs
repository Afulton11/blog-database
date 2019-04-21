using Common;
using DatabaseFactory.Data.Contracts;
using Domain.Business.CommandServices;
using Domain.Data.Commands;
using EnsureThat;
using System.Collections.Generic;
using System.Data;

namespace DataAccess.CommandServices
{
    public abstract class DbCommandService<TCommand> : ICommandService<TCommand>
        where TCommand : ICommand
    {
        public DbCommandService(IDatabase database)
        {
            EnsureArg.IsNotNull(database, nameof(database));

            Database = database;
        }

        public virtual void Execute(TCommand command)
        {
            Database.TryExecuteTransaction((transaction) =>
            {
                var procedure = Database.CreateStoredProcCommand(ProcedureName, transaction);
                procedure.Parameters.AddAll(GetParameters(command));

                Database.Execute(procedure);
            });
        }

        protected abstract IEnumerable<IDataParameter> GetParameters(TCommand command);
        protected IDatabase Database { get; }
        protected abstract string ProcedureName { get; }
    }
}
