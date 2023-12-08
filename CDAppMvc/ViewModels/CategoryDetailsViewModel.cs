namespace CDAppMvc.ViewModels;

public class CategoryDetailsViewModel
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public required string OwnerId { get; set; }
    public required string Owner { get; set; }
}