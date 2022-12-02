using LR.Models;
using Microsoft.AspNetCore.Mvc;


namespace LR.Components
{
  [ViewComponent]
  public class MenuViewComponent : ViewComponent
  {
    private readonly List<MenuItem> _menuItems = new()
        {
            new MenuItem {Controller = "Home", Action = "Index", Text = "Lab 2"},
            new MenuItem {Controller = "Movie", Action = "Index", Text = "Catalog"},
            new MenuItem{ Controller="Admin", Action="Index", Text = "Administration" }
        };

    public IViewComponentResult Invoke()
    {
      this.Check();
      return View(this._menuItems);
    }

    private void Check()
    {
      var controller = ViewContext.RouteData.Values["controller"]?.ToString();
      var area = ViewContext.RouteData.Values["area"]?.ToString();

      foreach (var menuItem in this._menuItems)
      {
        var controllerMatch = controller != null && controller == menuItem.Controller;
        var areaMatch = area != null && area == menuItem.Area;

        if (controllerMatch || areaMatch) menuItem.Active = "ACTIVE";
      }
    }
  }
}