using Serilog;

namespace LR.Models
{
  public class LogMiddleware
  {
    private readonly RequestDelegate _next;

    public LogMiddleware(RequestDelegate next) => this._next = next;

    public async Task InvokeAsync(HttpContext context)
    {
      await _next.Invoke(context);

      //Console.WriteLine($"STATUS: {context.Response.StatusCode}");
      if (context.Response.StatusCode != 200)
        Log.Information($"DATE {DateTime.Today} | URL {context.Request.Path} | QUERY {context.Request.QueryString} | RETURNS STATUS CODE {context.Response.StatusCode}");
    }
  }
}
