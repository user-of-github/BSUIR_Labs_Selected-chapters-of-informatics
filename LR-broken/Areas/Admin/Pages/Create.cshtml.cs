using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using LR.Entities;
using LR.Data;


namespace LR.Areas.Admin
{
  public class CreateModel : PageModel
  {
    private readonly MovieContext _context;

    public CreateModel(MovieContext context) => this._context = context;


    public IActionResult OnGet()
    {
      ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Id");
      return Page();
    }

    [BindProperty]
    public Movie Movie { get; set; }

    public async Task<IActionResult> OnPostAsync()
    {
      if (!ModelState.IsValid)
        return Page();


      _context.Movies.Add(Movie);
      await _context.SaveChangesAsync();

      return RedirectToPage("./Index");
    }
  }
}