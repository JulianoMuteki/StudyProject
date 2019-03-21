using Microsoft.Extensions.DependencyInjection;
using StudyProject.Application;
using StudyProject.Domain.Interfaces.Application;

namespace StudyProject.CrossCutting.Ioc
{
    public class ApplicationBootStrapperModule
    {
        public static void RegisterServices(IServiceCollection services)
        {
            //services.AddScoped<IProductApplicationService, ProductApplicationService>();
            services.AddScoped<IClientApplicationService, ClientApplicationService>();
        }
    }
}

