using Microsoft.EntityFrameworkCore;
using TemplateApi.Models;

namespace TemplateApi.Data;

public class AppDbContext : DbContext
{
    public DbSet<Item> Itens { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt)
    {
    }
}
