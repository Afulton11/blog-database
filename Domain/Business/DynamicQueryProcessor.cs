using Domain.Business.QueryServices;
using Domain.Data.Queries;
using EnsureThat;
using SimpleInjector;

namespace Domain.Business
{
    public class DynamicQueryProcessor : IQueryProcessor
    {
        private readonly Container container;

        public DynamicQueryProcessor(Container container)
        {
            EnsureArg.IsNotNull(container, nameof(container));

            this.container = container;
        }

        public TResult Execute<TResult>(IQuery<TResult> query)
        {
            var serviceType = typeof(IQueryService<,>).MakeGenericType(query.GetType(), typeof(TResult));

            dynamic service = this.container.GetInstance(serviceType);

            return service.Execute((dynamic)query);
        }
    }
}
