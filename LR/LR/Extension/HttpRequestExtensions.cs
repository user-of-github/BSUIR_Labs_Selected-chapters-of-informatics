namespace Microsoft.AspNetCore.Mvc
{
  public static class HttpRequestExtensions
  {
    public static bool IsAjaxRequest(this HttpRequest request) => request.Headers["x-requested-with"].ToString().ToLower().Equals("xmlhttprequest");
  }
}