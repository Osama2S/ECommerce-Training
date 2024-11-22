using API.DTO;
using AutoMapper;
using Core.Entities;
using Core.Entities.Identity;
using Core.Entities.OrderAggregate;


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
            CreateMap<Core.Entities.Identity.Address,AddressDTO>().ReverseMap();
            CreateMap<AddressDTO, Core.Entities.OrderAggregate.Address>();
            CreateMap<Order, OrderToReturnDto>()
                .ForMember(d=>d.DeliveryMethod,o=>o.MapFrom(s=>s.DeliveryMethod!.ShortName))
                .ForMember(d=>d.ShippingPrice,o=>o.MapFrom(s=>s.DeliveryMethod!.Price));
            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(d => d.ProductId, o => o.MapFrom(s => s.ItemOrdered!.ProductItemId))
                .ForMember(d => d.ProductName, o => o.MapFrom(s => s.ItemOrdered!.ProductName))
                .ForMember(d => d.PictureUrl, o => o.MapFrom(s => s.ItemOrdered!.PictureUrl))
                .ForMember(d => d.PictureUrl, o => o.MapFrom(s => PictureURL(s.ItemOrdered!.PictureUrl!)));
            
        }
        private string PictureURL(string pictureUrl)
        {
            if(!string.IsNullOrEmpty(pictureUrl))
                return "https://localhost:7081/"+pictureUrl;
            return null!;
        }
    }
}
