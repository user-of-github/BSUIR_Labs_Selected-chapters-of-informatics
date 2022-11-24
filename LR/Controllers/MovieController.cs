using LR.Data;
using LR.Entities;
using LR.Models;
using Microsoft.AspNetCore.Mvc;


namespace LR.Controllers
{
  public class MovieController : Controller
  {
    private MovieContext _context;
    private int _amountPerPage = 3;

    public MovieController(MovieContext context) =>  this._context = context;

    [Route("Catalog")]
    [Route("Catalog/Page_{currentPage:int}/{group:int?}")]
    public IActionResult Index(int? group, int currentPage = 1)
    {
      ViewData["Categories"] = _context.Categories.ToList();
      ViewData["CurrentCategory"] = group ?? 0;
      var moviesCount = _context.Movies.Where(movie => movie != null).Count();
      Console.WriteLine($"COUNT: {moviesCount}");
      var temp = moviesCount / _amountPerPage + 1;
      ListViewModel<Movie>.TotalPages = System.Convert.ToInt32(temp);

      return View(ListViewModel<Movie>.GetModel(_context.Movies, currentPage, movie => !group.HasValue || movie.Category.Id == group.Value));
    }
  }
}

