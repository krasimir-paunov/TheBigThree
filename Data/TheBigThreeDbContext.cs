using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using TheBigThree.Models;

namespace TheBigThree.Data
{
    public class TheBigThreeDbContext : IdentityDbContext
    {
        public TheBigThreeDbContext(DbContextOptions<TheBigThreeDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Game> Games { get; set; } = null!;
        public virtual DbSet<Genre> Genres { get; set; } = null!;
        public virtual DbSet<Collection> Collections { get; set; } = null!;
        public virtual DbSet<Like> Likes { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Like>()
                .HasKey(l => new { l.UserId, l.CollectionId });

            builder.Entity<Like>()
                .HasOne(l => l.Collection)
                .WithMany()
                .HasForeignKey(l => l.CollectionId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Genre>().HasData(
                new Genre { Id = 1, Name = "Action" },
                new Genre { Id = 2, Name = "Adventure" },
                new Genre { Id = 3, Name = "RPG" },
                new Genre { Id = 4, Name = "Strategy" },
                new Genre { Id = 5, Name = "First-Person Shooter" },
                new Genre { Id = 6, Name = "Sports" },
                new Genre { Id = 7, Name = "Racing" },
                new Genre { Id = 8, Name = "Horror" },
                new Genre { Id = 9, Name = "Platformer" },
                new Genre { Id = 10, Name = "Simulation" },
                new Genre { Id = 11, Name = "Fighting" },
                new Genre { Id = 12, Name = "Puzzle" },
                new Genre { Id = 13, Name = "MMORPG" },
                new Genre { Id = 14, Name = "Stealth" },
                new Genre { Id = 15, Name = "Survival" }
                    );
        }
    }
}
