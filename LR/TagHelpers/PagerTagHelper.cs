using LR.Entities;
using LR.Models;


namespace LR.TagHelper
{
  using Microsoft.AspNetCore.Razor.TagHelpers;


  public class PagerTagHelper : TagHelper
  {
    [HtmlAttributeName("page-current")]
    public int CurrentPage { get; set; }


    [HtmlAttributeName("page-total")]
    public int TotalPage { get; set; }


    [HtmlAttributeName("action")]
    public string ActionName { get; set; }


    [HtmlAttributeName("controller")]
    public string ControllerName { get; set; }


    [HtmlAttributeName("group-id")]
    public int GroupId { get; set; }


    private ListViewModel<Movie> _list;
    private LinkGenerator _linkGenerator;


    public PagerTagHelper(LinkGenerator linkGenerator) => this._linkGenerator = linkGenerator;


    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
      output.TagName = "li";

      if (ListViewModel<Movie>.CurrentPage == CurrentPage)
        output.Attributes.SetAttribute("class", "page-item active");
      else
        output.Attributes.SetAttribute("class", "page-item");

      string listContent = $"<a class=\"page-link\" href=\"/Catalog/Page_{this.CurrentPage}\">{this.CurrentPage}</a>";
      output.Content.SetHtmlContent(listContent);
    }
  }
}