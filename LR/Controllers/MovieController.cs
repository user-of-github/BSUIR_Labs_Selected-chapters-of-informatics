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
    

    public IActionResult Index(int? group, int currentPage = 1)
    {
      ViewData["Categories"] = _context.Categories.ToList();
      ViewData["CurrentCategory"] = group ?? 0;

      return View(ListViewModel<Movie>.GetModel(_context.Movies, currentPage, _amountPerPage, movie => !group.HasValue || movie.Category.Id == group.Value));
    }
  }
}

