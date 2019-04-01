using AutoMapper;
using StudyProject.Application.ViewModels;
using StudyProject.Domain.Entities;

namespace StudyProject.Application.AutoMapper
{
    public class ClientProfile : Profile
    {
        public ClientProfile()
        {
            CreateMap<Client, ClientVM>();
            CreateMap<ClientVM, Client>();
        }
    }
}
