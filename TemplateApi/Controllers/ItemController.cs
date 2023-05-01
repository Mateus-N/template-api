using Microsoft.AspNetCore.Mvc;
using TemplateApi.Dtos;
using TemplateApi.Models;
using TemplateApi.Services;

namespace TemplateApi.Controllers;

[ApiController]
[Route("[controller]")]
public class ItemController : ControllerBase
{
    private readonly ItemService itemService;
    private readonly ILogger<ItemController> logger;

    public ItemController(ItemService itemService, ILogger<ItemController> logger)
    {
        this.itemService = itemService;
        this.logger = logger;
    }

    [HttpPost]
    public async Task<IActionResult> CreateItem(CreateItemDto itemDto)
    {
        logger.LogInformation("Cadastrando item");
        try
        {
            Item item = await itemService.CreateItem(itemDto);
            logger.LogDebug("Item cadastrado com sucesso", item);
            return CreatedAtAction(nameof(RecuperaPorId), new { id = item.Id }, item);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Erro ao cadastrar o item");
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> RecuperaPorId(Guid id)
    {
        logger.LogInformation($"Consultando item Id={id}");
        try
        {
            ReadItemDto? readDto = await itemService.RecuperaPorId(id);
            if (readDto != null)
            {
                logger.LogDebug("Retornando item", readDto);
                return Ok(readDto);
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, $"Erro ao consultar item ID={id}");
            return BadRequest(ex.Message);
        }

        logger.LogWarning("O item não foi localizado");
        return NotFound();
    }
}
