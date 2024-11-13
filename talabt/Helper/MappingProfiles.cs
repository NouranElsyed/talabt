using AutoMapper;
using Microsoft.Extensions.Configuration;
using talabat.Core.Entities;
using talabat.Core.Entities.Identity;
using talabat.Core.Entities.Order_Aggregate;
using talabt.DTOs;
using talabtAPIs.DTOs;
using talabtAPIs.Helper;

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
            CreateMap<Order, OrderToReturnDto>()
                .ForMember(D => D.DeliveryMethod, O => O.MapFrom(N => N.DeliveryMethod.ShortName))
                .ForMember(D => D.DeliveryMethodCost, O => O.MapFrom(C => C.DeliveryMethod.Cost));
            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(I => I.ProductId, O => O.MapFrom(I => I.Product.ProductId))
                .ForMember(I => I.ProductName, O => O.MapFrom(I => I.Product.ProductName))
                .ForMember(I => I.ProductUrl, O => O.MapFrom(I => I.Product.ProductUrl))
                .ForMember(I => I.ProductUrl,O=>O.MapFrom<OrderItemPictureUrlResolver>());


        }
    }
}
