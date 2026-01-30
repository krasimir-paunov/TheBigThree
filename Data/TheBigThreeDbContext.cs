using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace TheBigThree.Data
{
    public class TheBigThreeDbContext : IdentityDbContext
    {
        public TheBigThreeDbContext(DbContextOptions<TheBigThreeDbContext> options)
            : base(options)
        {
        }
    }
}
