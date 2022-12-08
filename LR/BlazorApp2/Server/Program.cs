using LR.Data;
using LR.Entities;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddDbContext<MovieContext>(options =>
               options.UseSqlServer(
                   builder.Configuration.GetConnectionString("DefaultConnection")));

MovieContext context = builder.Services.BuildServiceProvider().GetService<MovieContext>();
DbInitializer.InitializeMovies(context);

builder.Host.ConfigureLogging(logging => {
  logging.ClearProviders();
  logging.AddConsole();
});

builder.Services.AddCors(opt =>
opt.AddPolicy("AllowAny",
builder => {
  builder.AllowAnyMethod()
  .AllowAnyHeader()
  .AllowAnyOrigin();

}));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseWebAssemblyDebugging();
}
else
{
  app.UseExceptionHandler("/Error");
  // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
  app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();


app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.UseCors("AllowAny");

app.Run();
