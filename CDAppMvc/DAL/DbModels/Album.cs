
using System.ComponentModel.DataAnnotations;

namespace CDAppMvc.DAL.DbModels;

public class Album
{
    public Guid Id { get; set; }

    [Required]
    [StringLength(50)]
    public required string Name { get; set; }

    [StringLength(500)]
    public string? Description { get; set; }

    [Required]
    [StringLength(100)]
    public required string Author { get; set; }

    [Required]
    public int CategoryId { get; set; }

    public int? ReleaseYear { get; set; }

    public virtual Category? Category { get; set; }

}