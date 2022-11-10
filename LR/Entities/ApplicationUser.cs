using Microsoft.AspNetCore.Identity;

namespace LR.Entities
{
  public class ApplicationUser : IdentityUser
  { 
    public byte[]? Image { get; set; }
    public string? ContentType { get; set; }

    public void InitializeArray(int length) => this.Image = new byte[length];
  }
}
