namespace StudyProject.CrossCutting.Ioc.DependencyInjection
{
    using Microsoft.Extensions.DependencyInjection;
    using StudyProject.Application;
    using StudyProject.Application.Services;
    using StudyProject.Domain.Interfaces.Application;

    /// <summary>
    /// ApplicationBootStrapperModule
    /// </summary>
    public static class ApplicationBootStrapperExtension
    {
        /// <summary>
        /// Registers the services.
        /// </summary>
        /// <param name="services">The services.</param>
        public static IServiceCollection RegisterApplicationBootStrapper(this IServiceCollection services)
        {
            services.AddScoped<IClientApplicationService, ClientApplicationService>();
            services.AddTransient<ICustomEmailSender, CustomEmailSender>();
            return services;
        }
    }
}