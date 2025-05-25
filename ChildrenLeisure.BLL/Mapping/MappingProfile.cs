using AutoMapper;
using ChildrenLeisure.BLL.DTOs;
using ChildrenLeisure.DAL.Entities;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ChildrenLeisure.BLL.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Attraction, AttractionDto>().ReverseMap();
            CreateMap<FairyCharacter, FairyCharacterDto>().ReverseMap();
            CreateMap<Order, OrderDto>().ReverseMap();
        }
    }
}
