using System.Runtime.InteropServices;
using Domain.Business.CommandServices.Decorators;
using Domain.Business.QueryServices.Decorators;
using DatabaseFactory.Config;
using DatabaseFactory.Data;
using DatabaseFactory.Data.Contracts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SimpleInjector;
using SimpleInjector.Integration.AspNetCore.Mvc;
using SimpleInjector.Lifestyles;
using Domain.Business.CommandServices;
using DataAccess.QueryServices.Readers;
using Domain.Business.QueryServices;
using DataAccess.DataAccess;
using Domain.Business;
using Domain.Entities.Blog;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Identity;
using Web.Services.Email;
using Web.Identity;

namespace Web
{
    public class Startup
    {
        private readonly Container container = new Container();

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDatabase<IDatabase, SQLDatabase>((provider, options) =>
            {
                var databaseSection = Configuration.GetSection("AppSettings");

                var connectionSection = RuntimeInformation.IsOSPlatform(OSPlatform.OSX)
                    ? databaseSection.GetSection("OSX")
                    : databaseSection.GetSection("Windows");
                
                var connectionString = connectionSection["ConnectionString"];

                options.UseConnectionString(connectionString);
            });

            services.AddSingleton<ICommandProcessor, DynamicAsyncCommandProcessor>();
            services.AddSingleton<IQueryProcessor, DynamicAsyncQueryProcessor>();

            services.AddTransient<IUserStore<User>, UserStore>();
            services.AddTransient<IRoleStore<Role>, RoleStore>();

            services.AddIdentity<User, Role>()
                .AddDefaultTokenProviders();

            services.Configure<EmailSettings>(Configuration.GetSection("EmailSettings"));
            services.AddTransient<IEmailSender, EmailSender>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            IntegrateSimpleInjector(services);
        }

        private void IntegrateSimpleInjector(IServiceCollection services)
        {
            container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();

            services.AddHttpContextAccessor();

            services.AddSingleton<IControllerActivator>(
                new SimpleInjectorControllerActivator(container));
            services.AddSingleton<IViewComponentActivator>(
                new SimpleInjectorViewComponentActivator(container));
            services.AddSingleton<IPageModelActivatorProvider>(
                new SimpleInjectorPageModelActivatorProvider(container));

            services.EnableSimpleInjectorCrossWiring(container);
            services.UseSimpleInjectorAspNetRequestScoping(container);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            InitializeContainer(app);

            container.Verify();
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc();
        }

        private void InitializeContainer(IApplicationBuilder app)
        {
            // Add application presentation components:
            container.RegisterMvcControllers(app);
            container.RegisterMvcViewComponents(app);
            container.RegisterPageModels(app);

            InitializeAppServices(app);

            // Allow Simple Injector to resolve services from ASP.NET Core.
            container.AutoCrossWireAspNetComponents(app);
        }

        private void InitializeAppServices(IApplicationBuilder app)
        {

            container.RegisterConditional(
                typeof(ILogger),
                c => typeof(Logger<>).MakeGenericType(c.Consumer.ImplementationType),
                Lifestyle.Singleton,
                c => true);


            var cqrsAssemblies = new[]
            {
                typeof(ICommandService<>).Assembly,
                typeof(IDataAccessAssemblyAccessor).Assembly,
            };

            container.Register(typeof(IReader<>), typeof(IReader<>).Assembly);
            container.Register(typeof(ICommandService<>), cqrsAssemblies);
            container.Register(typeof(IQueryService<,>), cqrsAssemblies);

            RegisterCommandServiceDecorators();
            RegisterQueryServiceDecorators();
        }

        private void RegisterCommandServiceDecorators()
        {
            container.RegisterDecorator(typeof(ICommandService<>), typeof(LoggerCommandServiceDecorator<>));
        }

        private void RegisterQueryServiceDecorators()
        {
            container.RegisterDecorator(typeof(IQueryService<,>), typeof(LoggerQueryServiceDecorator<,>));
        }
    }
}
