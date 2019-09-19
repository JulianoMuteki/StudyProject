namespace StudyProject.CrossCutting.Ioc.DependencyInjection
{
    using Microsoft.Extensions.DependencyInjection;
    using StudyProject.Domain.Interfaces.Base;
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
            services.AddTransient<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}