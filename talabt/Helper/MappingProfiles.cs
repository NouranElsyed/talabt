using AutoMapper;
using Microsoft.Extensions.Configuration;
using talabat.Core.Entities;
using talabat.Core.Entities.Identity;
using talabat.Core.Entities.Order_Aggregate;
using talabt.DTOs;
using talabtAPIs.DTOs;

namespace talabt.Helper
{
    public class MappingProfiles:Profile
    {
       
        public MappingProfiles()
        {
           
            CreateMap<Products, ProductDTO>()
                .ForMember(P => P.Brand, O => O.MapFrom(S => S.Brand.Name))
                .ForMember(P => P.Category, O => O.MapFrom(S => S.Category.Name))
                .ForMember(P => P.PictureUrl, O => O.MapFrom<ProductPictureUrlResolver>()).ReverseMap();
            CreateMap<talabat.Core.Entities.Identity.Address, AddressDTO>().ReverseMap();
            CreateMap<CustomerBasketDTO, CustomerBasket>().ReverseMap();
            CreateMap<BasketItemDTO, BasketItem>().ReverseMap();
            CreateMap<AddressDTO, talabat.Core.Entities.Order_Aggregate.Address>().ReverseMap();
        }
    }
}
