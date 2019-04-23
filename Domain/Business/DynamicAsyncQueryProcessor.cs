using System.Threading.Tasks;
using Domain.Business.QueryServices;
using Domain.Data.Queries;
using EnsureThat;
using SimpleInjector;

namespace Domain.Business
{
    public class DynamicAsyncQueryProcessor : IQueryProcessor
    {
        private readonly Container container;

        public DynamicAsyncQueryProcessor(Container container)
        {
            EnsureArg.IsNotNull(container, nameof(container));

            this.container = container;
        }

        public Task<TResult> Execute<TResult>(IQuery<TResult> query)
        {
            var serviceType = typeof(IQueryService<,>).MakeGenericType(query.GetType(), typeof(TResult));

            dynamic service = this.container.GetInstance(serviceType);

            return Task.Factory.StartNew<TResult>(() =>
            {
                return service.Execute((dynamic)query);
            });
        }
    }
}
