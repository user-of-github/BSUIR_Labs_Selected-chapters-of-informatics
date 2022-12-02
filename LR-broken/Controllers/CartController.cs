using LR.Data;
using Microsoft.AspNetCore.Mvc;
using LR.Models;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;

namespace LR.Controllers
{
  [Controller]
  public class CartController : Controller
  {
    private MovieContext _context;
    private Cart _cart = new();

    public CartController(MovieContext context, Cart cart) => (this._context, this._cart) = (context, cart);

    public IActionResult Index() => View(_cart.Items.Values);


    [Authorize]
    public IActionResult Add(int id, string returnUrl)
    {
      if (HttpContext.Session.GetString("cart") != null)
        this._cart = JsonConvert.DeserializeObject<Cart>(HttpContext.Session.GetString("cart"));
      

      var movie = this._context.Movies.Find(id);

      if (movie != null)
      {
        this._cart.AddToCart(movie);
        HttpContext.Session.SetString("cart", JsonConvert.SerializeObject(this._cart));
      }


      return Redirect(returnUrl);
    }

    [Authorize]
    public async Task<IActionResult> Delete(int id)
    {
      if (id == null || this._context.Movies == null)
        return NotFound();

      this._cart.Items.TryGetValue(id, out CartItem? item);

      if (item == null)
        return NotFound();

      return View(item);
    }

    [Authorize]
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
      if (HttpContext.Session.GetString("cart") != null)
        this._cart = JsonConvert.DeserializeObject<Cart>(HttpContext.Session.GetString("cart"));

      this._cart.RemoveFromCart(id);

      HttpContext.Session.SetString("cart", JsonConvert.SerializeObject(this._cart));

      return RedirectToAction(nameof(Index));
    }
  }
}