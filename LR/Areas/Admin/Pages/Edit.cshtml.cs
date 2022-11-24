using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LR.Data;
using LR.Entities;


namespace LR.Areas.Admin
{
  public class EditModel : PageModel
  {
    private readonly MovieContext _context;

    public EditModel(MovieContext context) => this._context = context;


    [BindProperty]
    public Movie Movie { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
      if (id == null || _context.Movies == null) return NotFound();

      var movie = await _context.Movies.FirstOrDefaultAsync(m => m.Id == id);

      if (movie == null) return NotFound();
      this.Movie = movie;

      ViewData["CategoryId"] = new SelectList(this._context.Categories, "Id", "Id");

      return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
      if (!ModelState.IsValid) return Page();

      this._context.Attach(this.Movie).State = EntityState.Modified;

      try
      {
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!BookExists(this.Movie.Id))
          return NotFound();
        else
          throw;
      }

      return RedirectToPage("./Index");
    }

    private bool BookExists(int id) => this._context.Movies.Any(e => e.Id == id);
  }
}