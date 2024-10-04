using API.DTO;
using AutoMapper;
using Core.Entities;

namespace API.Helpper
{
    public class ProfileAutoMapper : Profile
    {
        public ProfileAutoMapper()
        {
            CreateMap<Product, ProductDTO>()
                .ForMember(dest => dest.productBrand, act => act.MapFrom(src => src.productBrand!.Name))
                .ForMember(dest => dest.productType, act => act.MapFrom(src => src.productType!.Name))
                .ForMember(dest => dest.pictureUrl, act => act.MapFrom(src => PictureURL(src.pictureUrl!)));
        }
        private string PictureURL(string pictureUrl)
        {
            return "https://localhost:7081/"+pictureUrl;
        }
    }
}
