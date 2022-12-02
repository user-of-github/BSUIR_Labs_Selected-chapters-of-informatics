using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using LR.Models;


namespace LR.Components
{
  [ViewComponent]
  public class CartViewComponent : ViewComponent
  {
    private Cart _cart = new();

    public CartViewComponent(Cart cart) => this._cart = cart;
    public IViewComponentResult Invoke()
    {
      if (HttpContext.Session.GetString("cart") != null)
        this._cart = JsonConvert.DeserializeObject<Cart>(HttpContext.Session.GetString("cart"));

      return View(this._cart);
    }
  }
}