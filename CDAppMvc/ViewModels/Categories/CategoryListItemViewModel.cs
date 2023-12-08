namespace CDAppMvc.ViewModels.Categories;

public class CategoryListItemViewModel
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public required string OwnerId { get; set; }
}