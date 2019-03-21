using AutoMapper;
using StudyProject.Application.ViewModels;
using StudyProject.Domain.Entities;

namespace StudyProject.Application.AutoMapper
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductVM>();
        }
    }
}
