using Microsoft.AspNetCore.Identity;
using MockQueryable.Moq;
using Moq;
using TheBigThree.Data.Models;
using TheBigThree.Services.Core.Repositories;

namespace TheBigThree.Tests.Services
{
    [TestFixture]
    public class ProfileServiceTests
    {
        private Mock<IRepository<Collection>> collectionRepositoryMock;
        private Mock<IRepository<Comment>> commentRepositoryMock;
        private Mock<UserManager<ApplicationUser>> userManagerMock;
        private TheBigThree.Services.ProfileService profileService;

        [SetUp]
        public void SetUp()
        {
            collectionRepositoryMock = new Mock<IRepository<Collection>>();
            commentRepositoryMock = new Mock<IRepository<Comment>>();

            var store = new Mock<IUserStore<ApplicationUser>>();
            userManagerMock = new Mock<UserManager<ApplicationUser>>(
                store.Object, null, null, null, null, null, null, null, null);

            profileService = new TheBigThree.Services.ProfileService(
                collectionRepositoryMock.Object,
                commentRepositoryMock.Object,
                userManagerMock.Object);
        }

        [Test]
        public async Task GetUserTotalStarsAsync_ReturnsStars_WhenUserHasCollection()
        {
            string userId = "user-123";

            List<Collection> collections = new List<Collection>
            {
                new Collection { Id = 1, UserId = userId, Title = "My Collection", TotalStars = 15 }
            };

            collectionRepositoryMock
                .Setup(r => r.All())
                .Returns(collections.AsQueryable().BuildMock());

            int result = await profileService.GetUserTotalStarsAsync(userId);

            Assert.That(result, Is.EqualTo(15));
        }

        [Test]
        public async Task GetUserTotalStarsAsync_ReturnsZero_WhenUserHasNoCollection()
        {
            string userId = "user-123";

            List<Collection> collections = new List<Collection>();

            collectionRepositoryMock
                .Setup(r => r.All())
                .Returns(collections.AsQueryable().BuildMock());

            int result = await profileService.GetUserTotalStarsAsync(userId);

            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        public async Task GetOwnCollectionPreviewAsync_ReturnsNulls_WhenUserHasNoCollection()
        {
            string userId = "user-123";

            List<Collection> collections = new List<Collection>();

            collectionRepositoryMock
                .Setup(r => r.All())
                .Returns(collections.AsQueryable().BuildMock());

            var result = await profileService.GetOwnCollectionPreviewAsync(userId);

            Assert.That(result.Title, Is.Null);
            Assert.That(result.Id, Is.Null);
            Assert.That(result.GameImages, Is.Empty);
        }

        [Test]
        public async Task GetOwnCollectionPreviewAsync_ReturnsPreview_WhenUserHasCollection()
        {
            string userId = "user-123";

            List<Collection> collections = new List<Collection>
            {
                new Collection
                {
                    Id = 1,
                    UserId = userId,
                    Title = "Sci-Fi and Soul",
                    Games = new List<Game>
                    {
                        new Game { Title = "Mass Effect 2", ImageUrl = "url1", Description = "desc1", Genre = new Genre { Name = "RPG" } },
                        new Game { Title = "Dark Souls", ImageUrl = "url2", Description = "desc2", Genre = new Genre { Name = "Action" } },
                        new Game { Title = "Cyberpunk 2077", ImageUrl = "url3", Description = "desc3", Genre = new Genre { Name = "RPG" } }
                    }
                }
            };

            collectionRepositoryMock
                .Setup(r => r.All())
                .Returns(collections.AsQueryable().BuildMock());

            var result = await profileService.GetOwnCollectionPreviewAsync(userId);

            Assert.That(result.Title, Is.EqualTo("Sci-Fi and Soul"));
            Assert.That(result.Id, Is.EqualTo(1));
            Assert.That(result.GameImages.Count, Is.EqualTo(3));
        }

        [Test]
        public async Task GetUserTotalStarsAsync_ReturnsCorrectStars_WhenUserHasMultipleCollections()
        {
            string userId = "user-123";

            List<Collection> collections = new List<Collection>
            {
                new Collection { Id = 1, UserId = userId, Title = "Collection One", TotalStars = 10 },
                new Collection { Id = 2, UserId = userId, Title = "Collection Two", TotalStars = 5 }
            };

            collectionRepositoryMock
                .Setup(r => r.All())
                .Returns(collections.AsQueryable().BuildMock());

            int result = await profileService.GetUserTotalStarsAsync(userId);

            Assert.That(result, Is.EqualTo(10));
        }

        [Test]
        public async Task GetOwnCollectionPreviewAsync_ReturnsEmptyGameImages_WhenCollectionHasNoGames()
        {
            string userId = "user-123";

            List<Collection> collections = new List<Collection>
            {
                new Collection
                {
                    Id = 1,
                    UserId = userId,
                    Title = "Empty Collection",
                    Games = new List<Game>()
                }
            };

            collectionRepositoryMock
                .Setup(r => r.All())
                .Returns(collections.AsQueryable().BuildMock());

            var result = await profileService.GetOwnCollectionPreviewAsync(userId);

            Assert.That(result.Title, Is.EqualTo("Empty Collection"));
            Assert.That(result.GameImages, Is.Empty);
        }

        [Test]
        public async Task GetPublicProfileAsync_ReturnsNull_WhenUserDoesNotExist()
        {
            List<ApplicationUser> users = new List<ApplicationUser>();

            userManagerMock
                .Setup(u => u.Users)
                .Returns(users.AsQueryable().BuildMock());

            var result = await profileService.GetPublicProfileAsync("nonexistent");

            Assert.That(result, Is.Null);
        }

        [Test]
        public async Task GetPublicProfileAsync_ReturnsProfile_WhenUserExistsWithCollection()
        {
            string userId = "user-123";
            string username = "GeraltOfRivia";

            List<ApplicationUser> users = new List<ApplicationUser>
            {
                new ApplicationUser
                {
                    Id = userId,
                    UserName = username,
                    AvatarUrl = "https://example.com/avatar.jpg",
                    CreatedOn = new DateTime(2024, 1, 15, 0, 0, 0, DateTimeKind.Utc)
                }
            };

            List<Collection> collections = new List<Collection>
            {
                new Collection
                {
                    Id = 1,
                    UserId = userId,
                    Title = "Masterpieces of Atmosphere",
                    TotalStars = 10,
                    Games = new List<Game>
                    {
                        new Game { ImageUrl = "url1" },
                        new Game { ImageUrl = "url2" },
                        new Game { ImageUrl = "url3" }
                    }
                }
            };

            List<Comment> comments = new List<Comment>();

            userManagerMock
                .Setup(u => u.Users)
                .Returns(users.AsQueryable().BuildMock());

            collectionRepositoryMock
                .Setup(r => r.All())
                .Returns(collections.AsQueryable().BuildMock());

            commentRepositoryMock
                .Setup(r => r.All())
                .Returns(comments.AsQueryable().BuildMock());

            var result = await profileService.GetPublicProfileAsync(username);

            Assert.That(result, Is.Not.Null);
            Assert.That(result!.Username, Is.EqualTo(username));
            Assert.That(result.TotalStars, Is.EqualTo(10));
            Assert.That(result.CollectionTitle, Is.EqualTo("Masterpieces of Atmosphere"));
            Assert.That(result.CollectionGameImages.Count, Is.EqualTo(3));
        }

        [Test]
        public async Task GetPublicProfileAsync_ReturnsProfile_WhenUserExistsWithoutCollection()
        {
            string userId = "user-456";
            string username = "NewUser";

            List<ApplicationUser> users = new List<ApplicationUser>
            {
                new ApplicationUser
                {
                    Id = userId,
                    UserName = username,
                    CreatedOn = new DateTime(2024, 6, 1, 0, 0, 0, DateTimeKind.Utc)
                }
            };

            List<Collection> collections = new List<Collection>();
            List<Comment> comments = new List<Comment>();

            userManagerMock
                .Setup(u => u.Users)
                .Returns(users.AsQueryable().BuildMock());

            collectionRepositoryMock
                .Setup(r => r.All())
                .Returns(collections.AsQueryable().BuildMock());

            commentRepositoryMock
                .Setup(r => r.All())
                .Returns(comments.AsQueryable().BuildMock());

            var result = await profileService.GetPublicProfileAsync(username);

            Assert.That(result, Is.Not.Null);
            Assert.That(result!.CollectionId, Is.Null);
            Assert.That(result.CollectionGameImages, Is.Empty);
            Assert.That(result.TotalStars, Is.EqualTo(0));
        }

        [Test]
        public async Task GetPublicProfileAsync_ReturnsCorrectRank_BasedOnStars()
        {
            string userId = "user-789";
            string username = "StarCollector";

            List<ApplicationUser> users = new List<ApplicationUser>
            {
                new ApplicationUser
                {
                    Id = userId,
                    UserName = username,
                    CreatedOn = new DateTime(2024, 3, 1, 0, 0, 0, DateTimeKind.Utc)
                }
            };

            List<Collection> collections = new List<Collection>
            {
                new Collection { Id = 1, UserId = userId, Title = "Test", TotalStars = 35, Games = new List<Game>() }
            };

            List<Comment> comments = new List<Comment>();

            userManagerMock
                .Setup(u => u.Users)
                .Returns(users.AsQueryable().BuildMock());

            collectionRepositoryMock
                .Setup(r => r.All())
                .Returns(collections.AsQueryable().BuildMock());

            commentRepositoryMock
                .Setup(r => r.All())
                .Returns(comments.AsQueryable().BuildMock());

            var result = await profileService.GetPublicProfileAsync(username);

            Assert.That(result, Is.Not.Null);
            Assert.That(result!.Rank, Is.EqualTo("Superstar Collector"));
        }
    }
}