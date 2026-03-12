using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TheBigThree.Data.Models;
using static System.Net.WebRequestMethods;

namespace TheBigThree.Data
{
    public class TheBigThreeDbContext : IdentityDbContext<ApplicationUser>
    {
        public TheBigThreeDbContext(DbContextOptions<TheBigThreeDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Game> Games { get; set; } = null!;
        public virtual DbSet<Genre> Genres { get; set; } = null!;
        public virtual DbSet<Collection> Collections { get; set; } = null!;
        public virtual DbSet<Like> Likes { get; set; } = null!;
        public virtual DbSet<Comment> Comments { get; set; } = null!;

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

            builder.Entity<Comment>()
                .HasOne(c => c.Collection)
                .WithMany(c => c.Comments)
                .HasForeignKey(c => c.CollectionId)
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
            string user3Id = "a1b2c3d4-1111-2222-3333-aabbccdd0001";
            string user4Id = "a1b2c3d4-1111-2222-3333-aabbccdd0002";
            string user5Id = "a1b2c3d4-1111-2222-3333-aabbccdd0003";
            string user6Id = "a1b2c3d4-1111-2222-3333-aabbccdd0004";
            string user7Id = "a1b2c3d4-1111-2222-3333-aabbccdd0005";
            var hasher = new PasswordHasher<ApplicationUser>();

            builder.Entity<ApplicationUser>().HasData(
                new ApplicationUser
                {
                    Id = user1Id,
                    UserName = "GeraltOfRivia",
                    NormalizedUserName = "GERALTOFRIVIA",
                    Email = "geralt@kaermorhen.com",
                    NormalizedEmail = "GERALT@KAERMORHEN.COM",
                    PasswordHash = hasher.HashPassword(null!, "Legend123!"),
                    AvatarUrl = "https://avatarfiles.alphacoders.com/148/thumb-1920-148846.jpg",
                },
                new ApplicationUser
                {
                    Id = user2Id,
                    UserName = "CommanderShepard",
                    NormalizedUserName = "COMMANDERSHEPARD",
                    Email = "shepard@normandy.com",
                    NormalizedEmail = "SHEPARD@NORMANDY.COM",
                    PasswordHash = hasher.HashPassword(null!, "Spectre123!"),
                    AvatarUrl = "https://images3.alphacoders.com/210/thumb-1920-210637.jpg"
                },
                new ApplicationUser
                {
                    Id = user3Id,
                    UserName = "SolidSnake",
                    NormalizedUserName = "SOLIDSNAKE",
                    Email = "snake@shadowmoses.com",
                    NormalizedEmail = "SNAKE@SHADOWMOSES.COM",
                    PasswordHash = hasher.HashPassword(null!, "Tactical123!"),
                    AvatarUrl = "https://images.alphacoders.com/135/thumb-1920-1359067.png"
                },
                new ApplicationUser
                {
                    Id = user4Id,
                    UserName = "MasterChief",
                    NormalizedUserName = "MASTERCHIEF",
                    Email = "chief@unsc.com",
                    NormalizedEmail = "CHIEF@UNSC.COM",
                    PasswordHash = hasher.HashPassword(null!, "Spartan123!"),
                    AvatarUrl = "https://images2.alphacoders.com/607/thumb-1920-607052.jpg"
                },
                new ApplicationUser
                {
                    Id = user5Id,
                    UserName = "ArthurMorgan",
                    NormalizedUserName = "ARTHURMORGAN",
                    Email = "arthur@dutchgang.com",
                    NormalizedEmail = "ARTHUR@DUTCHGANG.COM",
                    PasswordHash = hasher.HashPassword(null!, "Outlaw123!"),
                    AvatarUrl = "https://images4.alphacoders.com/136/thumb-1920-1360121.jpeg"
                },
                new ApplicationUser
                {
                    Id = user6Id,
                    UserName = "LaraCroft",
                    NormalizedUserName = "LARACROFT",
                    Email = "lara@croftmanor.com",
                    NormalizedEmail = "LARA@CROFTMANOR.COM",
                    PasswordHash = hasher.HashPassword(null!, "Tomb123!"),
                    AvatarUrl = "https://images3.alphacoders.com/716/thumb-1920-716660.jpg"
                },
                new ApplicationUser
                {
                    Id = user7Id,
                    UserName = "AloyHorizon",
                    NormalizedUserName = "ALOYHORIZON",
                    Email = "aloy@horizon.com",
                    NormalizedEmail = "ALOY@HORIZON.COM",
                    PasswordHash = hasher.HashPassword(null!, "Nora123!"),
                    AvatarUrl = "https://images6.alphacoders.com/883/thumb-1920-883187.jpg"
                }
            );

            builder.Entity<Collection>().HasData(
                new Collection
                {
                    Id = 1,
                    Title = "Masterpieces of Atmosphere",
                    UserId = user1Id,
                    TotalStars = 5,
                    CreatedOn = DateTime.UtcNow.AddDays(-15)
                },
                new Collection
                {
                    Id = 2,
                    Title = "Sci-Fi and Soul",
                    UserId = user2Id,
                    TotalStars = 3,
                    CreatedOn = DateTime.UtcNow.AddDays(-12)
                },
                new Collection
                {
                    Id = 3,
                    Title = "Stealth Legends",
                    UserId = user3Id,
                    TotalStars = 8,
                    CreatedOn = DateTime.UtcNow.AddDays(-10)
                },
                new Collection
                {
                    Id = 4,
                    Title = "Shooters That Defined a Generation",
                    UserId = user4Id,
                    TotalStars = 12,
                    CreatedOn = DateTime.UtcNow.AddDays(-8)
                },
                new Collection
                {
                    Id = 5,
                    Title = "Open World Masterpieces",
                    UserId = user5Id,
                    TotalStars = 15,
                    CreatedOn = DateTime.UtcNow.AddDays(-6)
                },
                new Collection
                {
                    Id = 6,
                    Title = "Indie Gems Worth Your Time",
                    UserId = user6Id,
                    TotalStars = 3,
                    CreatedOn = DateTime.UtcNow.AddDays(-3)
                },
                new Collection
                {
                    Id = 7,
                    Title = "Horror That Kept Me Up at Night",
                    UserId = user7Id,
                    TotalStars = 7,
                    CreatedOn = DateTime.UtcNow.AddDays(-1)
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
                },
                new Game
                {
                    Id = 7,
                    Title = "Metal Gear Solid 3",
                    Description = "Metal Gear Solid 3: Snake Eater is a masterpiece of stealth game design set against the backdrop of the Cold War. Following the mission of Naked Snake deep in the Soviet jungle, the game introduces a survival and camouflage system that demands creative thinking and patience. Its emotionally devastating finale remains one of the most impactful moments in gaming history, cementing Big Boss as one of the medium's greatest tragic heroes.",
                    ImageUrl = "/images/seed/mgs3.jpg",
                    CollectionId = 3,
                    GenreId = 14
                },
                new Game
                {
                    Id = 8,
                    Title = "Dishonored",
                    Description = "Dishonored is a brilliantly designed immersive sim that places player agency above all else. As Corvo Attano, a supernatural assassin framed for murder, the game offers a staggering variety of approaches to every encounter — from ghost-like pacifist runs to chaotic high-power takedowns. Its Victorian-industrial city of Dunwall is one of gaming's most atmospheric settings, dripping with political intrigue and dark lore.",
                    ImageUrl = "/images/seed/dishonored.jpg",
                    CollectionId = 3,
                    GenreId = 14
                },
                new Game
                {
                    Id = 9,
                    Title = "Hitman: Blood Money",
                    Description = "Hitman: Blood Money represents the peak of the sandbox assassination formula. Each level is an intricately designed puzzle box filled with creative opportunities to eliminate targets in increasingly elaborate ways. The game rewards patience, observation, and improvisation, allowing Agent 47 to blend seamlessly into any environment. Its iconic missions and darkly comedic tone make it an enduring classic of the stealth genre.",
                    ImageUrl = "/images/seed/hitman.jpg",
                    CollectionId = 3,
                    GenreId = 14
                },
                new Game
                {
                    Id = 10,
                    Title = "Halo 2",
                    Description = "Halo 2 fundamentally changed what was expected of a console first-person shooter. Its dual-wielding mechanics, switching narrative between Master Chief and the Arbiter, and the introduction of online multiplayer via Xbox Live created a template that the industry followed for years. The cliffhanger ending may have frustrated fans at the time, but the game's impact on competitive online gaming is still felt to this day.",
                    ImageUrl = "/images/seed/halo2.jpg",
                    CollectionId = 4,
                    GenreId = 5
                },
                new Game
                {
                    Id = 11,
                    Title = "DOOM (2016)",
                    Description = "DOOM 2016 revitalized the first-person shooter genre by stripping away cover mechanics and regenerating health in favor of relentless, aggressive combat. The glory kill system encourages constant forward momentum, rewarding fearless play with health and ammo. Mick Gordon's iconic industrial metal soundtrack perfectly complements the non-stop carnage, making this one of the most purely satisfying shooters ever created.",
                    ImageUrl = "/images/seed/doom2016.jpg",
                    CollectionId = 4,
                    GenreId = 5
                },
                new Game
                {
                    Id = 12,
                    Title = "Counter-Strike: Global Offensive",
                    Description = "Counter-Strike: Global Offensive is the definitive competitive first-person shooter, refined over decades into a game of pure mechanical skill and tactical teamwork. Its economy system, precise gunplay, and iconic maps like Dust2 have created a competitive ecosystem unlike any other. The game demands absolute precision and communication, rewarding dedicated players with a depth of mastery that few shooters can match.",
                    ImageUrl = "/images/seed/csgo.jpg",
                    CollectionId = 4,
                    GenreId = 5
                },
                new Game
                {
                    Id = 13,
                    Title = "Red Dead Redemption 2",
                    Description = "Red Dead Redemption 2 is Rockstar's magnum opus — a sprawling, painstakingly detailed portrait of the dying American frontier. Arthur Morgan's journey is one of the most emotionally resonant narratives in gaming, exploring themes of loyalty, redemption, and the inevitable march of progress. Every corner of its vast world feels handcrafted and alive, from chance encounters on muddy trails to the bustling streets of Saint Denis.",
                    ImageUrl = "/images/seed/rdr2.jpg",
                    CollectionId = 5,
                    GenreId = 16
                },
                new Game
                {
                    Id = 14,
                    Title = "The Legend of Zelda: Breath of the Wild",
                    Description = "Breath of the Wild reinvented the open world genre by creating a world governed by consistent physical rules rather than scripted events. Every element of Hyrule can be interacted with, climbed, burned, or frozen, giving players unprecedented freedom to approach any situation creatively. Its philosophy of discovery over hand-holding restored a sense of childlike wonder to gaming that had been largely absent from big-budget titles for years.",
                    ImageUrl = "/images/seed/botw.jpg",
                    CollectionId = 5,
                    GenreId = 2
                },
                new Game
                {
                    Id = 15,
                    Title = "Grand Theft Auto V",
                    Description = "Grand Theft Auto V is an unprecedented achievement in open world design, featuring three interwoven protagonists navigating the sun-soaked excess and moral bankruptcy of Los Santos. Its heist missions represent some of the most ambitious set pieces in gaming history, while the living, breathing city remains a benchmark for environmental detail and emergent storytelling. The game's enduring popularity is a testament to the sheer density of its world.",
                    ImageUrl = "/images/seed/gtav.jpg",
                    CollectionId = 5,
                    GenreId = 16
                },
                new Game
                {
                    Id = 16,
                    Title = "Hollow Knight",
                    Description = "Hollow Knight is a triumph of independent game development, delivering a vast and hauntingly beautiful underground kingdom filled with challenging combat, intricate platforming, and a melancholic lore told through environmental storytelling. Team Cherry's masterpiece rivals the best of the Metroidvania genre, offering dozens of hours of exploration across a world that constantly rewards curiosity and punishes complacency.",
                    ImageUrl = "/images/seed/hollowknight.jpg",
                    CollectionId = 6,
                    GenreId = 18
                },
                new Game
                {
                    Id = 17,
                    Title = "Hades",
                    Description = "Hades redefined what a roguelike could be by weaving a compelling narrative directly into its endlessly replayable structure. Each failed escape attempt from the Underworld advances the story and deepens relationships with a cast of brilliantly written mythological characters. Supergiant Games achieved something extraordinary — a game where dying feels as rewarding as succeeding, ensuring every run leaves the player hungry for one more attempt.",
                    ImageUrl = "/images/seed/hades.jpg",
                    CollectionId = 6,
                    GenreId = 17
                },
                new Game
                {
                    Id = 18,
                    Title = "Celeste",
                    Description = "Celeste is a precision platformer that uses its brutally challenging gameplay as a metaphor for mental health struggles, delivering one of gaming's most sincere and affecting narratives. Madeline's climb up the titular mountain mirrors her internal battle with anxiety and self-doubt, creating a rare synergy between mechanics and story. Its tight controls and brilliantly designed levels make every hard-won summit feel like a genuine personal achievement.",
                    ImageUrl = "/images/seed/celeste.jpg",
                    CollectionId = 6,
                    GenreId = 9
                },
                new Game
                {
                    Id = 19,
                    Title = "Resident Evil 2 Remake",
                    Description = "The Resident Evil 2 Remake is a near-perfect reimagining of a survival horror classic, updating its fixed-camera predecessor into a tense over-the-shoulder nightmare without sacrificing any of the original's atmosphere or tension. The relentless pursuit of Mr. X creates a constant sense of dread that permeates every corridor of the Raccoon City Police Department. It stands as a blueprint for how to respectfully modernize a beloved classic.",
                    ImageUrl = "/images/seed/re2remake.jpg",
                    CollectionId = 7,
                    GenreId = 8
                },
                new Game
                {
                    Id = 20,
                    Title = "Amnesia: The Dark Descent",
                    Description = "Amnesia: The Dark Descent stripped away combat entirely, leaving players with nothing but darkness, limited resources, and the terrifying sounds of pursuing monsters. Its sanity mechanic punishes even looking at threats for too long, creating a uniquely psychological horror experience. The game fundamentally changed the horror genre and inspired an entire generation of atmospheric, narrative-driven horror titles.",
                    ImageUrl = "/images/seed/amnesia.jpg",
                    CollectionId = 7,
                    GenreId = 8
                },
                new Game
                {
                    Id = 21,
                    Title = "Outlast",
                    Description = "Outlast drops players into the nightmare of Mount Massive Asylum armed with nothing but a camcorder and their nerve. The absence of any combat mechanics transforms every encounter into pure flight-or-hide survival, creating sustained tension that few horror games have matched. Seen entirely through the grainy green glow of night vision, its corridors filled with disturbing imagery make it one of the most viscerally unsettling horror experiences in gaming.",
                    ImageUrl = "/images/seed/outlast.jpg",
                    CollectionId = 7,
                    GenreId = 8
                }
            );
        }
    }
}