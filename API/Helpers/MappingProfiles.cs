using API.Dtos;
using AutoMapper;
using Core.Entities;

namespace API.Helpers
{
    public class MappingProfiles : Profile
    {  
        //Helps to map the properties of the Product entity to the ProductToReturnDto
        public MappingProfiles()
        {
            CreateMap<Product, ProductToReturnDto>()
            //Maps the ProductBrand and ProductType properties of the Product entity to the ProductBrand and ProductType properties of the ProductToReturnDto
            .ForMember(d => d.ProductBrand, o => o.MapFrom(s => s.ProductBrand.Name))
            .ForMember(d => d.ProductType, o => o.MapFrom(s => s.ProductType.Name))
            .ForMember(d => d.PictureUrl, o => o.MapFrom<ProductUrlResolver>());
        }
    }
}