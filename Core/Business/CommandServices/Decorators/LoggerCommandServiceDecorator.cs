using DatabaseFactory.Data.Contracts;
using Microsoft.Extensions.Logging;

namespace Core.Business.CommandServices.Decorators
{
    /// <summary>
    /// Logs information about the execution of the decoratee.
    /// </summary>
    /// <typeparam name="TCommand"></typeparam>
    public sealed class LoggerCommandServiceDecorator<TCommand> : ICommandService<TCommand>
        where TCommand : ICommand
    {
        private readonly ICommandService<TCommand> decoratee;
        private readonly ILogger logger;

        public LoggerCommandServiceDecorator(ICommandService<TCommand> decoratee, ILogger logger)
        {
            this.decoratee = decoratee;
            this.logger = logger;
        }

        public void Execute(TCommand command)
        {
            logger.LogInformation($"{decoratee.GetType().Name} has started executing.");
            this.decoratee.Execute(command);
            logger.LogInformation($"${decoratee.GetType().Name} has executed successfully.");
        }
    }
}
