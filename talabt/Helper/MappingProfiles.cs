using AutoMapper;
using talabat.Core.Entities;
using talabt.DTOs;

namespace talabt.Helper
{
    public class MappingProfiles:Profile
    {
        public MappingProfiles()
        {
            CreateMap<Products, ProductDTO>()
                .ForMember(P => P.Brand, O => O.MapFrom(S => S.Brand.Name))
                .ForMember(P => P.Category, O => O.MapFrom(S => S.Category.Name));
        }
    }
}
