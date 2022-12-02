using Microsoft.AspNetCore.Routing;
using LR.Entities;
using LR.Models;


namespace LR.TagHelpers
{

  using Microsoft.AspNetCore.Razor.TagHelpers;


  public class ImageTagHelper : TagHelper
  {
    [HtmlAttributeName("img-controller")]
    public string ControllerName { get; set; }

    [HtmlAttributeName("img-action")]
    public string ActionName { get; set; }


    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
      output.TagName = "img";
      output.Attributes.SetAttribute("style", "width: 30px; height: 30px; border-radius: 5em; margin-left:20px;");
      output.Attributes.SetAttribute("src", $"/{this.ControllerName}/{this.ActionName}");
      output.Attributes.SetAttribute("alt", "User");
    }
  }
}