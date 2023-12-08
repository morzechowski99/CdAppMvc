using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace CDAppMvc.DAL.DbModels;

public class Category
{
    public int Id { get; set; }

    [StringLength(20)]
    [Required]
    public required string Name { get; set; }

    [StringLength(200)]
    public string? Description { get; set; }

    [Required]
    public required string OwnerId { get; set; }

    public virtual IdentityUser? Owner { get; set; }

    public virtual ICollection<Album>? Albums { get; set; }
}