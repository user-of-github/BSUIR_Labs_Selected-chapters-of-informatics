﻿using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.NetworkInformation;
using LR.Data;

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

  public class DbInitializer
  {
    public static async Task InitializeAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
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

    public static async void Initialize(WebApplication app)
    {
      var serviceProvider = app.Services.CreateScope().ServiceProvider;
      var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
      var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

      await DbInitializer.InitializeAsync(userManager, roleManager);
    }

    public static async void InitializeMovies(MovieContext context)
    {
      var prequels = new Category { Title = "Prequel" };
      var sequels = new Category { Title = "Sequel" };
      var solo = new Category { Title = "Solo film" };
      var crossovers = new Category { Title = "Crossovers" };

      if (!context.Categories.Any())
      {
        context.Categories.AddRange(prequels, sequels, solo, crossovers);
        context.SaveChanges();
      }

      if (!context.Movies.Any())
      {
        context.Movies.AddRange(
          new Movie { Category = sequels, Title = "Black panther 2", Description = "Shuri becomes Black Panther", Duration = 150, ImagePath = "images/bp2.webp" },
          new Movie { Category = crossovers, Title = "Avengers: EndGame", Description = "The film which had almost 3 billion $ box office", Duration = 170, ImagePath = "images/a4.webp" },
          new Movie { Category = solo, Title = "Crown", Description = "no description", Duration = 0, ImagePath = "images/icon.png" },
          new Movie { Category = sequels, Title = "Ant Man 3: Quantumania", Description = "Kang is excellent", Duration = 120, ImagePath = "images/am3.jpg" },
          new Movie { Category = crossovers, Title = "Avengers 5: Kang's dynasty", Description="Kang !!!", Duration = 170, ImagePath="images/a3.jpg"}

        );
        context.SaveChanges();
      }
    }
  }

}

