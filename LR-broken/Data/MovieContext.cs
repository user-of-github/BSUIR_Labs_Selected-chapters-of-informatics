using LR.Entities;
using Microsoft.EntityFrameworkCore;


namespace LR.Data
{
  public class MovieContext : DbContext
  {
    public DbSet<Movie> Movies { get; set; }
    public DbSet<Category> Categories { get; set; }

    public MovieContext(DbContextOptions<MovieContext> options): base(options) => Database.EnsureCreated();    
  }
}
