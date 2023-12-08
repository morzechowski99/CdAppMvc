using CDAppMvc.DAL.DbModels;
using System.ComponentModel.DataAnnotations;

namespace CDAppMvc.ViewModels.Albums;

public class CreateUpdateAlbumViewModel
{
    [Required]
    [StringLength(50)]
    public required string Name { get; set; }

    [StringLength(500)]
    public string? Description { get; set; }

    [Required]
    [StringLength(100)]
    public required string Author { get; set; }

    [Required]
    [Display(Name = "Category")]
    public int CategoryId { get; set; }

    [Range(0, 2050)]
    [Display(Name = "Release Year")]
    public int? ReleaseYear { get; set; }

}