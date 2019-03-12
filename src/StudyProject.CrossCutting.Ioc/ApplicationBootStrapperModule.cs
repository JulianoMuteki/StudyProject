using Microsoft.Extensions.DependencyInjection;
using StudyProject.Application;
using StudyProject.Domain.Services;

namespace StudyProject.CrossCutting.Ioc
{
    public class ApplicationBootStrapperModule
    {
        public static void RegisterServices(IServiceCollection services)
        {
          
            services.AddScoped<IClientApplicationService, ClientApplicationService>();
        }
    }
}
