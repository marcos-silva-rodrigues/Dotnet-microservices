using AutoMapper;
using ItemService.Dtos;
using ItemService.Models;

namespace ItemService.Profiles
{
    public class ItemProfile : Profile
    {
        public ItemProfile()
        {
            CreateMap<Restaurante, RestauranteReadDto>();
            CreateMap<ItemCreateDto, Item>();
            CreateMap<Item, ItemCreateDto>();
            CreateMap<RestauranteReadDto, Restaurante>()
                .ForMember(dest => dest.IdExterno, opt => opt.MapFrom(src => src.Id));
        }
    }
}