using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LR.Data;
using LR.Entities;


namespace LR.Controllers
{
  [Controller]
  public class AdminController : Controller
  {
    private readonly MovieContext _context;
    private IWebHostEnvironment _appEnvironment;

    public AdminController(MovieContext context, IWebHostEnvironment appEnvironment) =>
      (this._context, this._appEnvironment) = (context, appEnvironment);

    // GET: Admin
    public async Task<IActionResult> Index() => View(await this._context.Movies.Include(b => b.Category).ToListAsync());


    // GET: Admin/Details/5
    public async Task<IActionResult> Details(int? id)
    {
      if (id == null || this._context.Movies == null) return NotFound();

      var movie = await this._context.Movies
          .Include(b => b.Category)
          .FirstOrDefaultAsync(m => m.Id == id);

      if (movie == null) return NotFound();


      return View(movie);
    }

    // GET: Admin/Create
    public IActionResult Create()
    {
      ViewData["Categories"] = new SelectList(_context.Categories, "Id", "Title");
      return View();
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,CategoryId,Title,Discription,Price, Image")] Movie movie)
    {
      if (ModelState.IsValid)
      {
        if (movie.Image != null)
        {
          string path = "/images/books/" + _context.Movies.Count() + ".jpg";

          using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
          {
            await movie.Image.CopyToAsync(fileStream);
          }

          movie.ImagePath = path;
        }

        this._context.Add(movie);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
      }
      ViewData["Categories"] = new SelectList(_context.Categories, "Id", "Title", movie.Category);

      return View(movie);
    }

    // GET: Admin/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
      if (id == null || this._context.Movies == null)
        return NotFound();

      var movie = await _context.Movies.FindAsync(id);

      if (movie == null)
        return NotFound();

      ViewData["Categories"] = new SelectList(_context.Categories, "Id", "Title", movie.Category);

      return View(movie);
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,CategoryId,Title,Description,Duration,Image")] Movie movie)
    {
      if (id != movie.Id)
        return NotFound();

      if (ModelState.IsValid)
      {
        try
        {
          if (movie.Image != null)
          {

            string? path = movie.ImagePath;
            if (path == null)
            {
              path = "/images/books/" + movie.Id + ".jpg";
              movie.ImagePath = path;
            }


            using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
            {
              await movie.Image.CopyToAsync(fileStream);
            }
          }

          _context.Update(movie);
          await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
          if (!BookExists(movie.Id))
            return NotFound();
          else
            throw;
        }

        return RedirectToAction(nameof(Index));
      }

      ViewData["Categories"] = new SelectList(_context.Categories, "Id", "Title", movie.Category);

      return View(movie);
    }

    // GET: Admin/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
      if (id == null || _context.Movies == null)
        return NotFound();

      var movie = await _context.Movies
          .Include(b => b.Category)
          .FirstOrDefaultAsync(m => m.Id == id);

      if (movie == null)
        return NotFound();

      return View(movie);
    }

    // POST: Admin/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
      if (this._context.Movies == null)
        return Problem("Entity set 'MovieContext.Movies'  is null.");


      var movie = await this._context.Movies.FindAsync(id);

      if (movie != null)
      {
        if (movie.ImagePath != null)
        {
          string? path = movie.ImagePath;
          System.IO.File.Delete(_appEnvironment.WebRootPath + path);
        }

        this._context.Movies.Remove(movie);
      }

      await _context.SaveChangesAsync();
      return RedirectToAction(nameof(Index));
    }

    private bool BookExists(int id) => this._context.Movies.Any(e => e.Id == id);
  }
}