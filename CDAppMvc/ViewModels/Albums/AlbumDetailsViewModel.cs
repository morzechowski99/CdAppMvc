using System.ComponentModel.DataAnnotations;

namespace CDAppMvc.ViewModels.Albums;

public class AlbumDetailsViewModel
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public required string Author { get; set; }
    [Display(Name="Release Year")]
    public int? ReleaseYear { get; set; }
    public string? Category { get; set; }
}