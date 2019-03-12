using AutoMapper;
using StudyProject.Domain.Entities;
using StudyProject.UI.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyProject.UI.Web.Mappers
{
    public class ClientProfile : Profile
    {
        public ClientProfile()
        {
            CreateMap<Client, ClientViewModel>();
        }
    }
}
