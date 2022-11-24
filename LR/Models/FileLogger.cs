namespace LR.Models
{
  public class FileLogger : ILogger
  {
    private string _filePath;
    private static object _lock = new();
    public FileLogger(string path) => this._filePath = path;

    public IDisposable BeginScope<TState>(TState state) => null;

    public bool IsEnabled(LogLevel logLevel) => true;

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
    {
      if (formatter != null)
      {
        lock (FileLogger._lock)
        {
          File.AppendAllText(_filePath, formatter(state, exception) + Environment.NewLine);
        }
      }
    }
  }
  public class FileLoggerProvider : ILoggerProvider
  {
    private string _path;
    public FileLoggerProvider(string path) => this._path = path;
    public ILogger CreateLogger(string categoryName) => new FileLogger(this._path);

    public void Dispose()
    {}
  }
  public static class FileLoggerExtensions
  {
    public static ILoggerFactory AddFile(this ILoggerFactory factory, string _filePath)
    {
      factory.AddProvider(new FileLoggerProvider(_filePath));
      return factory;
    }
  }


  public class LogMiddleware
  {
    private readonly RequestDelegate _next;

    public LogMiddleware(RequestDelegate next) => this._next = next;

    public async Task InvokeAsync(HttpContext context, ILoggerFactory loggerFactory)
    {
      await _next.Invoke(context);

      loggerFactory.AddFile(Path.Combine(Directory.GetCurrentDirectory(), "logger.txt"));
      var logger = loggerFactory.CreateLogger("FileLogger");

      if (context.Response.StatusCode != 200)
        logger.LogInformation($"{DateTime.Today}: {context.Request.Path}  -- {context.Request.QueryString} --  {context.Response.StatusCode}");
    }
  }
}