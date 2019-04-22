using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Domain.Business.CommandServices;
using Domain.Data.Commands;
using EnsureThat;
using SimpleInjector;

namespace Domain.Business
{
    public class DynamicAsyncCommandProcessor : ICommandProcessor
    {
        private readonly Container container;

        public DynamicAsyncCommandProcessor(Container container)
        {
            EnsureArg.IsNotNull(container);
            this.container = container;
        }

        public Task Execute(ICommand command)
        {
            var serviceType = typeof(ICommandService<>).MakeGenericType(typeof(ICommand));

            dynamic service = container.GetInstance(serviceType);

            return Task.Factory.StartNew(() =>
            {
                service.Execute((dynamic)command);
            });
        }
    }
}
