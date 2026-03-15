using Microsoft.AspNetCore.Identity;

namespace TheBigThree.Data.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? AvatarUrl { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
