namespace StudyProject.CrossCutting.Ioc.DependencyInjection
{
    using Microsoft.Extensions.DependencyInjection;
    using StudyProject.Domain.Interfaces;
    using StudyProject.Infra.Repository;
    using StudyProject.Infra.Repository.Common;

    /// <summary>
    /// InfraBootStrapperModule
    /// </summary>
    public static class InfraBootStrapperExtension
    {
        /// <summary>
        /// Registers the services.
        /// </summary>
        /// <param name="services">The services.</param>
        public static IServiceCollection RegisterInfraBootStrapper(this IServiceCollection services)
        {
            // Infra - Data
            //services.AddScoped<ICustomerRepository, CustomerRepository>();
            //services.AddScoped<IUnitOfWork, UnitOfWork>();
            //services.AddScoped<EquinoxContext>();

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddTransient<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
