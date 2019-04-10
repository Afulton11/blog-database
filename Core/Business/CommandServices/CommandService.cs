using DatabaseFactory.Data.Contracts;
using Microsoft.Extensions.Logging;

namespace Core.Business.CommandServices
{
    public abstract class CommandService<TCommand> : ICommandService<TCommand> where TCommand : ICommand
    {
        protected CommandService(ILogger logger)
        {
            Logger = logger;
        }

        protected ILogger Logger { get; }
        public abstract void Execute(TCommand command);
    }
}
