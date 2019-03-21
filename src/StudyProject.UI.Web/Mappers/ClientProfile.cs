using AutoMapper;
using StudyProject.Domain.Entities;
using StudyProject.UI.Web.Models;

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