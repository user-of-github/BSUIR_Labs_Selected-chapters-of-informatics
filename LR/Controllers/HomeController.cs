using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;

namespace LR.Controllers;

[Controller]
public class HomeController : Controller
{
    public class ListDemo
    {
        public int ListItemValue { get; set; }
        public string ListItemText { get; set; }
    }

    private readonly ILogger<HomeController> _logger;
    private readonly List<ListDemo> _listDemo;

    public HomeController(ILogger<HomeController> logger)
    {
        this._listDemo = new List<ListDemo>
        {
            new ListDemo {ListItemValue = 1, ListItemText = "Item 1"},
            new ListDemo {ListItemValue = 2, ListItemText = "Item 2"},
            new ListDemo {ListItemValue = 3, ListItemText = "Item 3"}
        };

        this._logger = logger;
    }

    public IActionResult Index()
    {
        ViewData["Text"] = "Laboratory work № 2";
        ViewData["Lst"] = new SelectList(_listDemo, "ListItemValue", "ListItemText");

        return View();
    }
}