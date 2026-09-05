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
            CreateMap<ProductVM, Product>()             
                           .ForMember(dest => dest.ClientsProductsValues, opt => opt.Ignore())
                           .AfterMap((src, dest) => dest.Init());
        }
    }
}
