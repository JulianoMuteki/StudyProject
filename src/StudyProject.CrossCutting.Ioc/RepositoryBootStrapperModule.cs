using Microsoft.Extensions.DependencyInjection;
using StudyProject.Domain.Interfaces.Base;
using StudyProject.Infra.Repository;
using StudyProject.Infra.Repository.Common;

namespace StudyProject.CrossCutting.Ioc
{
    public class RepositoryBootStrapperModule
    {
        public static void RegisterServices(IServiceCollection services)
        {
            // Infra - Data
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            //helper service
            services.AddScoped<IUnitOfWork, UnitOfWork>();

        }
    }
}
