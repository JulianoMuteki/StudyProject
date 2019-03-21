using Microsoft.Extensions.DependencyInjection;
using StudyProject.Domain.Interfaces.Base;
using StudyProject.Infra.Repository;
using StudyProject.Infra.Repository.Common;

namespace StudyProject.CrossCutting.Ioc
{
    public class InfraBootStrapperModule
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddTransient<IUnitOfWork, UnitOfWork>();
        }
    }
}
