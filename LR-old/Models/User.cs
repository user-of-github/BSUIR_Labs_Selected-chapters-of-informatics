using Microsoft.AspNetCore.Identity;


namespace LR.Models
{
    public class User : IdentityUser
    {
        public byte[]? Avatar { get; set; }
    }
}