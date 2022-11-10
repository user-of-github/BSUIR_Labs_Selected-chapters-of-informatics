using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.NetworkInformation;

namespace LR.Entities
{
  [NotMapped]
  [Table("dbo.AspNetUsers")]
  public class ApplicationUser : IdentityUser
  { 
    public byte[]? Image { get; set; }
    public string? ContentType { get; set; }

    public void InitializeArray(int length) => this.Image = new byte[length];
  }

  public class DbInitializer {
        public static async Task InitializeAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager) {
            const string adminEmail = "admin123@gmail.com";
            const string adminPassword = "testpassword";

            const string usualUserEmail = "user2022@gmail.com";
            const string usualUserPassword = "testpassword2";

            if (await roleManager.FindByIdAsync("user") == null)
                await roleManager.CreateAsync(new IdentityRole("user"));
            
            if (await roleManager.FindByIdAsync("admin") == null) 
                await roleManager.CreateAsync(new IdentityRole("admin"));
            

            if (await userManager.FindByNameAsync(adminEmail) == null) 
            {
                var admin = new ApplicationUser { Email = adminEmail, UserName = adminEmail, EmailConfirmed = true };
                var result = await userManager.CreateAsync(admin, adminPassword);
                if (result.Succeeded) await userManager.AddToRoleAsync(admin, "admin");
            }

            if (await userManager.FindByNameAsync(usualUserEmail) == null) 
            {
                var user = new ApplicationUser { Email = usualUserPassword, UserName = usualUserEmail, EmailConfirmed = true };
                var result = await userManager.CreateAsync(user, usualUserPassword);
                if (result.Succeeded) await userManager.AddToRoleAsync(user, "user");
            }
        }

        public static async void Initialize(WebApplication app) {
            var serviceProvider = app.Services.CreateScope().ServiceProvider;
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            await DbInitializer.InitializeAsync(userManager, roleManager);
        }

    }
}
