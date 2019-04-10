using System;
using System.Linq;
using System.Reflection;
using DatabaseFactory.Config.Builder;
using DatabaseFactory.Data;
using EnsureThat;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace DatabaseFactory.Config
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDatabase<TContext>(
            this IServiceCollection services,
            ServiceLifetime contextLifetime = ServiceLifetime.Scoped,
            ServiceLifetime optionsLifetime = ServiceLifetime.Scoped)
            where TContext : Database =>
                services.AddDatabase<TContext, TContext>(contextLifetime, optionsLifetime);

        public static IServiceCollection AddDatabase<TContextService, TContextImpl>(
            this IServiceCollection services,
            ServiceLifetime contextLifetime = ServiceLifetime.Scoped,
            ServiceLifetime optionsLifetime = ServiceLifetime.Scoped)
            where TContextImpl : Database, TContextService
            where TContextService : class =>
                services.AddDatabase<TContextService, TContextImpl>(
                    (Action<IServiceProvider, IDatabaseOptionsBuilder>)null,
                    contextLifetime,
                    optionsLifetime);

        public static IServiceCollection AddDatabase<TContext>(
            this IServiceCollection services,
            Action<IServiceProvider, IDatabaseOptionsBuilder> optionsAction,
            ServiceLifetime contextLifetime = ServiceLifetime.Scoped,
            ServiceLifetime optionsLifetime = ServiceLifetime.Scoped)
            where TContext : Database =>
                services.AddDatabase<TContext, TContext>(optionsAction, contextLifetime, optionsLifetime);

        public static IServiceCollection AddDatabase<TContextService, TContextImpl>(
            this IServiceCollection services,
            Action<IServiceProvider, IDatabaseOptionsBuilder> optionsAction,
            ServiceLifetime contextLifetime = ServiceLifetime.Scoped,
            ServiceLifetime optionsLifetime = ServiceLifetime.Scoped)
            where TContextImpl : Database, TContextService
        {
            EnsureArg.IsNotNull(services, nameof(services));

            if (contextLifetime == ServiceLifetime.Singleton)
            {
                optionsLifetime = ServiceLifetime.Singleton;
            }

            if (optionsAction != null)
            {
                CheckContextConstructors<TContextImpl>();
            }

            // register database dependencies
            AddCoreDatabaseServices<TContextImpl>(services, optionsAction, optionsLifetime);

            // register the database now that dependencies can be found by DI
            services.TryAdd(
                new ServiceDescriptor(
                    typeof(TContextService),
                    typeof(TContextImpl),
                    contextLifetime));

            return services;
        }

        /// <summary>
        /// Adds the core database services. These being the DatabaseOptions that the database uses.
        /// </summary>
        private static void AddCoreDatabaseServices<TContextImpl>(
            IServiceCollection services,
            Action<IServiceProvider, IDatabaseOptionsBuilder> optionsAction,
            ServiceLifetime optionsLifetime)
            where TContextImpl : Database
        {
            services.TryAdd(
                new ServiceDescriptor(
                    typeof(DatabaseOptions<TContextImpl>),
                    provider => DatabaseOptionsFactory<TContextImpl>(provider, optionsAction),
                    optionsLifetime));

            services.TryAdd(
                new ServiceDescriptor(
                    typeof(DatabaseOptions),
                    provider => provider.GetRequiredService<DatabaseOptions<TContextImpl>>(),
                    optionsLifetime));
        }

        private static DatabaseOptions<TContext> DatabaseOptionsFactory<TContext>(
            IServiceProvider serviceProvider,
            Action<IServiceProvider, IDatabaseOptionsBuilder> optionsAction)
            where TContext : Database
        {
            var builder = DatabaseOptionsBuilder<TContext>.CreateInstance();

            optionsAction.Invoke(serviceProvider, builder);

            return builder.Options;
        }

        private static void CheckContextConstructors<TContext>()
            where TContext : Database
        {
            var declaredConstructors = typeof(TContext).GetTypeInfo().DeclaredConstructors.ToList();
            if (declaredConstructors.Count == 1
                && declaredConstructors[0].GetParameters().Length == 0)
            {
                var contextType = typeof(TContext).Name;
                throw new ArgumentException(
$"AddDatabase was called with configuration, but the context type '{contextType}' only declares a parameterless constructor. " +
	"This means that the configuration passed to AddDatabase will never be used. {contextType} should declare a constructor " +
	"that accepts a DatabaseContextOptions&lt;TContext&gt; object in its constructor, passing it to the base constructor."
                    );
            }
        }

    }
}
