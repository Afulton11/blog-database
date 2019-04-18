using Domain.Data.Commands;
using Microsoft.Extensions.Logging;

namespace Domain.Business.CommandServices.Decorators
{
    /// <summary>
    /// Logs information about the execution of the decoratee.
    /// </summary>
    /// <typeparam name="TCommand"></typeparam>
    public sealed class LoggerCommandServiceDecorator<TCommand> : ICommandService<TCommand>
        where TCommand : ICommand
    {
        private readonly ILogger logger;
        private readonly ICommandService<TCommand> decoratee;

        public LoggerCommandServiceDecorator(
            ILogger<ICommandService<TCommand>> logger,
            ICommandService<TCommand> decoratee)
        {
            this.logger = logger;
            this.decoratee = decoratee;
        }

        public void Execute(TCommand command)
        {
            logger.LogInformation($"{decoratee.GetType().Name} has started executing.");
            this.decoratee.Execute(command);
            logger.LogInformation($"${decoratee.GetType().Name} has executed successfully.");
        }
    }
}
