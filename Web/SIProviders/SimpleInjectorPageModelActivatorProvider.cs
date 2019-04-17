using Microsoft.AspNetCore.Mvc.RazorPages;
using SimpleInjector;
using System;

namespace Web.SIProviders
{
    /// <summary>
    /// https://stackoverflow.com/questions/50065258/simple-injector-inject-into-pagemodel-asp-net-core-razor-pages
    /// Answered by Steven himself.
    /// </summary>
    public class SimpleInjectorPageModelActivatorProvider : IPageModelActivatorProvider
    {
        private Container Container { get; }
        public SimpleInjectorPageModelActivatorProvider(Container c) => Container = c;
        public Func<PageContext, object> CreateActivator(CompiledPageActionDescriptor d) =>
            _ => Container.GetInstance(d.ModelTypeInfo.AsType());
        public Action<PageContext, object> CreateReleaser(CompiledPageActionDescriptor d) =>
            null;
    }
}
