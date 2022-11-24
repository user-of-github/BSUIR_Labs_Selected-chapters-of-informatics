using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using RequiredAttribute = System.ComponentModel.DataAnnotations.RequiredAttribute;


namespace LR.Entities
{
  public class Movie
  {
    public int Id { get; set; }
     
    public Category? Category { get; set; }

    [Display(Name="Title")]
    [Required]
    [StringLength(100, MinimumLength = 5)]
    public string? Title { get; set; }

    public string Description { get; set; }

    [Required]
    public int Duration { get; set; }

    [Display(Name = "Avatar")]
    [NotMapped]
    public IFormFile Image { get; set; }

    public string? ImagePath { get; set; }

    public string? MimeType { get; set; }
  }

  public class Category
  {
    public int Id { get; set; }
    public string? Title { get; set; }

    public ICollection<Movie>? Movies{ get; set; }
  }
}
