using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using LR.Data;
using LR.Entities;


namespace LR.Areas.Admin
{
  public class IndexModel : PageModel
  {
    private readonly MovieContext _context;

    public IndexModel(MovieContext context) => this._context = context;

    public IList<Movie> Movie { get; set; } = default!;

    public async Task OnGetAsync()
    {
      if (this._context.Movies != null)
        this.Movie = await _context.Movies.Include(b => b.Category).ToListAsync();
    }
  }
}