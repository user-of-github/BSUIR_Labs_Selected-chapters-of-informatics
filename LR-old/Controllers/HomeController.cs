using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;

namespace LR.Controllers
{
    
    
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
                new ListDemo {ListItemValue = 1, ListItemText = "Item 1 (first)"},
                new ListDemo {ListItemValue = 2, ListItemText = "Item 2 (second)"},
                new ListDemo {ListItemValue = 3, ListItemText = "Item 3 (third)"}
            };

            this._logger = logger;
        }
        
        [Route("")]
        [Route("/")]
        [Route("~/", Name = "default")]
        public IActionResult Index()
        {
            ViewData["List"] = new SelectList(_listDemo, "ListItemValue", "ListItemText");
            ViewBag.Text = "Laboratory work № 2";

            return View();
        }
    }
}

