using AutoMapper;
using TemplateApi.Data;
using TemplateApi.Dtos;
using TemplateApi.Models;

namespace TemplateApi.Services;

public class ItemService
{
    private readonly AppDbContext context;
    private readonly IMapper mapper;

    public ItemService(AppDbContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    public async Task<Item> CreateItem(CreateItemDto itemDto)
    {
        Item item = mapper.Map<Item>(itemDto);
        await context.Itens.AddAsync(item);
        context.SaveChanges();
        return item;
    }

    public async Task<ReadItemDto?> RecuperaPorId(Guid id)
    {
        Item? item = await context.Itens.FindAsync(id);
        if (item == null) return null;
        return mapper.Map<ReadItemDto?>(item);
    }
}
