using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TheBigThree.Data.Models;

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
                new Genre { Id = 15, Name = "Survival" },
                new Genre { Id = 16, Name = "Action/Adventure" },
                new Genre { Id = 17, Name = "Roguelike" },
                new Genre { Id = 18, Name = "Metroidvania" },
                new Genre { Id = 19, Name = "Soulslike" }
            );

            string user1Id = "48668352-3932-411a-966a-123456789012";
            string user2Id = "7c13958c-362c-4493-979d-098765432109";
            var hasher = new PasswordHasher<IdentityUser>();

            builder.Entity<IdentityUser>().HasData(
                new IdentityUser
                {
                    Id = user1Id,
                    UserName = "GeraltOfRivia",
                    NormalizedUserName = "GERALTOFRIVIA",
                    Email = "geralt@kaermorhen.com",
                    NormalizedEmail = "GERALT@KAERMORHEN.COM",
                    PasswordHash = hasher.HashPassword(null!, "Legend123!")
                },
                new IdentityUser
                {
                    Id = user2Id,
                    UserName = "CommanderShepard",
                    NormalizedUserName = "COMMANDERSHEPARD",
                    Email = "shepard@normandy.com",
                    NormalizedEmail = "SHEPARD@NORMANDY.COM",
                    PasswordHash = hasher.HashPassword(null!, "Spectre123!")
                }
            );

            builder.Entity<Collection>().HasData(
                new Collection
                {
                    Id = 1,
                    Title = "Masterpieces of Atmosphere",
                    UserId = user1Id,
                    TotalStars = 5,
                    CreatedOn = DateTime.UtcNow.AddDays(-5)
                },
                new Collection
                {
                    Id = 2,
                    Title = "Sci-Fi and Soul",
                    UserId = user2Id,
                    TotalStars = 1,
                    CreatedOn = DateTime.UtcNow.AddDays(-2)
                }
            );

            builder.Entity<Game>().HasData(
                new Game
                {
                    Id = 1,
                    Title = "The Witcher 3",
                    Description = "The Witcher 3: Wild Hunt is a genre-defining open-world RPG that sets a monumental standard for narrative depth and world-building. Following the journey of Geralt of Rivia, the game weaves a complex tapestry of political intrigue, personal loss, and mythical horror. Every side quest feels like a handcrafted story, often presenting morally grey choices that ripple throughout the Continent, ensuring that no two playthroughs ever feel exactly the same.",
                    ImageUrl = "/images/seed/witcher3.jpg",
                    CollectionId = 1,
                    GenreId = 3
                },
                new Game
                {
                    Id = 2,
                    Title = "Elden Ring",
                    Description = "Elden Ring is a masterclass in environmental storytelling and player agency, born from the legendary collaboration between FromSoftware and George R.R. Martin. The Lands Between offers an unparalleled sense of discovery, where every horizon hides a secret, a challenge, or a piece of haunting lore. Its combat system is deep and rewarding, allowing for endless build variety while maintaining the signature 'tough but fair' difficulty that defines the Soulslike genre.",
                    ImageUrl = "/images/seed/eldenring.jpg",
                    CollectionId = 1,
                    GenreId = 19
                },
                new Game
                {
                    Id = 3,
                    Title = "Silent Hill 2",
                    Description = "Silent Hill 2 remains the gold standard for psychological horror, utilizing atmosphere and symbolism to explore the darkest corners of the human psyche. The journey of James Sunderland is not merely a survival horror experience but a deeply personal exploration of guilt, punishment, and redemption. Supported by Akira Yamaoka's legendary score and an oppressive fog that conceals grotesque manifestations of trauma, it is a game that haunts the player long after the credits roll.",
                    ImageUrl = "/images/seed/silenthill2.jpg",
                    CollectionId = 1,
                    GenreId = 8
                },
                new Game
                {
                    Id = 4,
                    Title = "Dark Souls",
                    Description = "The original Dark Souls is a landmark achievement in level design, featuring a world that is intricately interconnected in ways that still baffle and delight players today. It revitalized the concept of discovery through trial and error, punishing recklessness while rewarding patience and observation. From the heights of Anor Londo to the depths of Blighttown, its somber atmosphere and cryptic lore created a legacy that spawned an entire sub-genre of modern action-RPGs.",
                    ImageUrl = "/images/seed/darksouls.jpg",
                    CollectionId = 2,
                    GenreId = 19
                },
                new Game
                {
                    Id = 5,
                    Title = "Mass Effect 2",
                    Description = "Mass Effect 2 represents the pinnacle of squad-based storytelling, focusing on the assembly of a diverse team of specialists for a high-stakes suicide mission against the Collectors. The game expertly balances fast-paced tactical combat with profound character interactions, where loyalty missions provide deep insight into your companions' pasts. The choices made by Commander Shepard carry immense weight, culminating in a finale that remains one of the most intense and emotional sequences in gaming history.",
                    ImageUrl = "/images/seed/masseffect2.jpg",
                    CollectionId = 2,
                    GenreId = 3
                },
                new Game
                {
                    Id = 6,
                    Title = "Cyberpunk 2077",
                    Description = "Cyberpunk 2077 offers a breathtakingly dense vision of a dystopian future within the neon-soaked sprawl of Night City. Playing as V, players navigate a dangerous world of corporate warfare, cybernetic enhancement, and mercenary survival. The narrative is a deeply personal one, exploring themes of identity and legacy alongside the digital ghost of Johnny Silverhand. With its vastly improved systems and immersive first-person perspective, it delivers a uniquely visceral and stylish RPG experience.",
                    ImageUrl = "/images/seed/cyberpunk2077.jpg",
                    CollectionId = 2,
                    GenreId = 3
                }
            );
        }
    }
}