using System.Threading.Tasks;
using Domain.Business.QueryServices;
using Domain.Data.Queries;
using EnsureThat;
using SimpleInjector;

namespace Domain.Business
{
    public class DynamicAsyncQueryProcessor : IAsyncQueryProcessor
    {
        private readonly Container container;

        public DynamicAsyncQueryProcessor(Container container)
        {
            EnsureArg.IsNotNull(container, nameof(container));

            this.container = container;
        }

        public Task<TResult> ExecuteAsync<TResult>(IQuery<TResult> query)
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
