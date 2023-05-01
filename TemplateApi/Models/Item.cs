using System.ComponentModel.DataAnnotations;

namespace TemplateApi.Models;

public class Item
{
    [Key]
    [Required]
    public Guid Id { get; set; }
    [Required]
    public string Name { get; set; }
}
