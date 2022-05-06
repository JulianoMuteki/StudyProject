using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using StudyProject.Application.AutoMapper;
using System;

namespace StudyProject.CrossCutting.Ioc.DependencyInjection
{
    public static class AutoMapperSetup
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        public static void AddAutoMapperSetup(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            // Registering Mappings automatically only works if the 
            // Automapper Profile classes are in ASP.NET project
            var mappingConfig = AutoMapperConfig.RegisterMappings();
            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
        }
    }
}
