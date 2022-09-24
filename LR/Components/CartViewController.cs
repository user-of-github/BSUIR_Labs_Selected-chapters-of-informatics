using Microsoft.AspNetCore.Mvc;

namespace LR.Components
{
    [ViewComponent]
    public class Cart : ViewComponent
    {
        public IViewComponentResult Invoke() => View();
    }
}