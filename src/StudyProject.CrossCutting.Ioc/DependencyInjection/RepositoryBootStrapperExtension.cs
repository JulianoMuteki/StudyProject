namespace StudyProject.CrossCutting.Ioc.DependencyInjection
{
    using Microsoft.Extensions.DependencyInjection;
    using StudyProject.Domain.Interfaces;
    using StudyProject.Infra.Repository;
    using StudyProject.Infra.Repository.Common;

    /// <summary>
    /// RepositoryBootStrapperModule
    /// </summary>
    public static class RepositoryBootStrapperExtension
    {
        /// <summary>
        /// Registers the services.
        /// </summary>
        /// <param name="services">The services.</param>
        public static IServiceCollection RegisterBootStrapper(this IServiceCollection services)
        {
            // Infra - Data
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            //helper service
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
