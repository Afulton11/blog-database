using Core.Business.Contracts;
using Core.Data.Queries;
using EnsureThat;
using Microsoft.Extensions.Logging;

namespace Core.Business.QueryServices.Decorators
{
    public class LoggerQueryServiceDecorator<TQuery, TResult> : IQueryService<TQuery, TResult>
        where TQuery : IQuery<TResult>
    {
        private readonly ILogger logger;
        private readonly IQueryService<TQuery, TResult> decoratee;

        public LoggerQueryServiceDecorator(
            ILogger<IQueryService<TQuery, TResult>> logger,
            IQueryService<TQuery, TResult> decoratee)
        {
            EnsureArg.IsNotNull(logger, nameof(logger));
            EnsureArg.IsNotNull(decoratee, nameof(decoratee));

            this.logger = logger;
            this.decoratee = decoratee;
        }

        public TResult Execute(TQuery query)
        {
            logger.LogInformation($"{decoratee.GetType().Name} has started executing.");

            var result = decoratee.Execute(query);

            logger.LogInformation($"{decoratee.GetType().Name} has executed sucessfully.");

            return result;
        }
    }
}
