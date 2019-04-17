using Core.Business.Contracts;
using Core.Data.Commands;
using DatabaseFactory.Data;

namespace Core.Business.CommandServices.Points
{
    public abstract class CreatePointService : ICommandService<CreatePointCommand>
    {
        protected CreatePointService(Database database)
        {
            Database = database;
        }

        protected abstract string ProcedureName { get; }
        protected Database Database { get; }

        public void Execute(CreatePointCommand command)
        {
            Database.TryExecuteTransaction((transaction) =>
            {
                var procedure = Database.CreateStoredProcCommand(ProcedureName, transaction);
                var userIdParameter = Database.CreateParameter("userId", command.UserId);

                procedure.Parameters.Add(userIdParameter);

                Database.Execute(procedure);
            });
        }

    }
}
