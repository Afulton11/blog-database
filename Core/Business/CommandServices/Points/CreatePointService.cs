using Core.Data.Commands;
using DatabaseFactory.Data;
using Microsoft.Extensions.Logging;

namespace Core.Business.CommandServices.Points
{
    public abstract class CreatePointService : CommandService<CreatePointCommand>
    {
        protected CreatePointService(ILogger logger, Database database) : base(logger)
        {
            Database = database;
        }

        protected abstract string ProcedureName { get; }
        protected Database Database { get; }

        public override void Execute(CreatePointCommand command)
        {
            Database.TryExecuteTransaction((transaction) =>
            {
                var procedure = Database.CreateStoredProcCommand(ProcedureName, transaction);
                var userIdParameter = Database.CreateParameter("userId", command.UserId);

                procedure.Parameters.Add(userIdParameter);

                Database.Execute(procedure);
            });
            Logger.LogInformation($"{this.GetType().Name} has been executed successfully.");
        }

    }
}
