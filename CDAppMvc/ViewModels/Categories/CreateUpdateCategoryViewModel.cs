using System.ComponentModel.DataAnnotations;

namespace CDAppMvc.ViewModels.Categories;

public class CreateUpdateCategoryViewModel
{
    [StringLength(20)]
    [Required]
    public required string Name { get; set; }

    [StringLength(200)]
    public string? Description { get; set; }

    public string OwnerId { get; set; } = string.Empty;
}