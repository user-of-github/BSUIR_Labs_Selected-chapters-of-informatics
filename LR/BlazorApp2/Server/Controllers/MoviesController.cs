using LR.Data;
using LR.Entities;
using LR.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BlazorApp2.Shared;


namespace BlazorApp2.Server.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class MoviesController : ControllerBase
  {
    private readonly ILogger _logger;
    private readonly MovieContext _context;

    public MoviesController(MovieContext context, ILogger<MoviesController> logger) => (this._context, this._logger) = (context, logger);

    [HttpGet]
    public async Task<ActionResult<ListViewModel>> GetBooks()
    {
      List<Movie> books = await _context.Movies.ToListAsync();

      ListViewModel list = new();

      list.listWithMovies = new();

      foreach (var item in books)
      {
        list.listWithMovies.Add(new DetailViewModel
        {
          Id = item.Id,
          CategoryId = item.CategoryId,
          Title = item.Title,
          Description = item.Description,
          Duration = item.Duration,
          ImagePath = item.ImagePath,
          MimeType = item.MimeType,
        });
      }

      return list;
    }

    // GET: api/Movies/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Movie>> GetMovie(int id)
    {
      var movie = await this._context.Movies.FindAsync(id);

      if (movie == null)

        return NotFound();

      return movie;
    }

    // PUT: api/Movies/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutBook(int id, Movie movie)
    {
      if (id != movie.Id)
        return BadRequest();

      this._context.Entry(movie).State = EntityState.Modified;

      try
      {
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!this.MovieExists(id))
          return NotFound();
        else
          throw;
      }

      return NoContent();
    }

    // POST: api/Movies
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Movie>> PostMovie(Movie movie)
    {
      this._context.Movies.Add(movie);
      await this._context.SaveChangesAsync();

      return CreatedAtAction("GetMovie", new { id = movie.Id }, movie);
    }

    // DELETE: api/Movies/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBook(int id)
    {
      var movie = await this._context.Movies.FindAsync(id);
      if (movie == null)
        return NotFound();
      

      this._context.Movies.Remove(movie);
      await this._context.SaveChangesAsync();

      return NoContent();
    }

    private bool MovieExists(int id) => this._context.Movies.Any(e => e.Id == id);
  }
}