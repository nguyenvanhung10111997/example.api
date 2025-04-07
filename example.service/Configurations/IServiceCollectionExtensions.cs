using Autofac;
using example.domain.Interfaces;
using example.infrastructure;
using example.infrastructure.Configurations;
using example.infrastructure.ContainerManager;
using example.infrastructure.Repositories;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace example.service.Configurations
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services)
        {
            services.AddDbContext<ExampleDbContext>(options =>
            {
                options.UseSqlServer(ApiConfig.Connection.DefaultConnectionString);
                options.UseLazyLoadingProxies();
            });

            services.AddScoped<ExampleDbContext>();
            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            return services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        }

        public static void RegisterServiceDependeny(this ContainerBuilder builder)
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies()
                .Where(x => x.GetName().Name.Contains("service") || x.GetName().Name.Contains("infrastructure"));

            foreach (var assembly in assemblies)
            {
                if (assembly != null)
                {
                    builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces().InstancePerLifetimeScope();
                }
            }

            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();
        }

        public static void AddMediator(this IServiceCollection services)
        {
            var assembly = AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(x => x.GetName().Name.Contains("service"));

            if (assembly != null)
            {
                services.AddMediatR(configuration => configuration.RegisterServicesFromAssembly(assembly));

                services.AddValidatorsFromAssembly(assembly);
            }
        }
    }
}
