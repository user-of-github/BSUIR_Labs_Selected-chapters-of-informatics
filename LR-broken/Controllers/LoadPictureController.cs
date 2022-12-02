using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using LR.Data;
using LR.Entities;


namespace LR.Controllers
{
  public class LoadPictureController : Controller
  {
    private UserManager<ApplicationUser>? _userManager;
    private IWebHostEnvironment? _webHostEnvironment;
    private ApplicationDbContext _context;
    private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

    public LoadPictureController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
    {
      _context = context;
      _userManager = userManager;
    }

    public async Task<IActionResult> GetImage([FromServices] Microsoft.AspNetCore.Hosting.IWebHostEnvironment env)
    {
      var user = await GetCurrentUserAsync();

      if (user != null && user.Image?.Length > 0) 
        return File(user.Image, user.ContentType);
      
      var provider = env.WebRootFileProvider;
      var path = Path.Combine("images", "icon.png");
      var fInfo = provider.GetFileInfo(path);
      var ext = Path.GetExtension(fInfo.Name);
      var extProvider = new FileExtensionContentTypeProvider();

      return File(fInfo.CreateReadStream(), extProvider.Mappings[ext]);
    }

    public IActionResult Index() => View(_context.Users.ToList());
  }
}
