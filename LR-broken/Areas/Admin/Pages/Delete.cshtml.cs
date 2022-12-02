using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using LR.Data;
using LR.Models;
using LR.Entities;



namespace LR.Areas.Admin
{
  public class DeleteModel : PageModel
  {
    private readonly MovieContext _context;

    public DeleteModel(MovieContext context) =>  _context = context;
    

    [BindProperty]
    public Movie Movie{ get; set; }

    public async Task<IActionResult> OnGetAsync(int? id)
    {
      if (id == null || _context.Movies == null) return NotFound();

      var movie = await _context.Movies.FirstOrDefaultAsync(m => m.Id == id);

      if (movie == null) 
        return NotFound();
      else 
        this.Movie = movie;

      return Page();
    }

    public async Task<IActionResult> OnPostAsync(int? id)
    {
      if (id == null || _context.Movies == null) return NotFound();

      var movie = await _context.Movies.FindAsync(id);

      if (movie != null)
      {
        this.Movie = movie;
        _context.Movies.Remove(Movie);
        await _context.SaveChangesAsync();
      }

      return RedirectToPage("./Index");
    }
  }
}