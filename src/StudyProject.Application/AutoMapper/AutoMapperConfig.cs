using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudyProject.Application.AutoMapper
{
    public class AutoMapperConfig
    {
        public static MapperConfiguration RegisterMappings()
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ClientProfile());
                cfg.AddProfile(new ProductProfile());
            });
        }
    }
}
