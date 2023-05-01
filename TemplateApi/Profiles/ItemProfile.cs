using AutoMapper;
using TemplateApi.Dtos;
using TemplateApi.Models;

namespace TemplateApi.Profiles;

public class ItemProfile : Profile
{
    public ItemProfile()
    {
        CreateMap<CreateItemDto, Item>();
        CreateMap<Item, ReadItemDto>();
    }
}
