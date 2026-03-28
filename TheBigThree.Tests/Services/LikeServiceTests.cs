using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using Moq;
using TheBigThree.Data;
using TheBigThree.Data.Models;
using TheBigThree.Services.Core.Repositories;
using TheBigThree.Web.ViewModels;

namespace TheBigThree.Tests.Services
{
    [TestFixture]
    public class LikeServiceTests
    {
        private Mock<IRepository<Like>> likeRepositoryMock;
        private Mock<IRepository<Collection>> collectionRepositoryMock;
        private TheBigThreeDbContext dbContext;
        private TheBigThree.Services.LikeService likeService;

        [SetUp]
        public void SetUp()
        {
            likeRepositoryMock = new Mock<IRepository<Like>>();
            collectionRepositoryMock = new Mock<IRepository<Collection>>();

            DbContextOptions<TheBigThreeDbContext> options = new DbContextOptionsBuilder<TheBigThreeDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            dbContext = new TheBigThreeDbContext(options);

            likeService = new TheBigThree.Services.LikeService(
                likeRepositoryMock.Object,
                collectionRepositoryMock.Object,
                dbContext);
        }

        [TearDown]
        public void TearDown()
        {
            dbContext.Dispose();
        }

        [Test]
        public async Task IsStarredByUserAsync_ReturnsTrue_WhenUserHasStarred()
        {
            string userId = "user-123";
            int collectionId = 1;

            List<Like> likes = new List<Like>
            {
                new Like { UserId = userId, CollectionId = collectionId }
            };

            likeRepositoryMock
                .Setup(r => r.All())
                .Returns(likes.AsQueryable().BuildMock());

            bool result = await likeService.IsStarredByUserAsync(collectionId, userId);

            Assert.That(result, Is.True);
        }

        [Test]
        public async Task IsStarredByUserAsync_ReturnsFalse_WhenUserHasNotStarred()
        {
            string userId = "user-123";

            int collectionId = 1;

            List<Like> likes = new List<Like>();

            likeRepositoryMock
                .Setup(r => r.All())
                .Returns(likes.AsQueryable().BuildMock());

            bool result = await likeService.IsStarredByUserAsync(collectionId, userId);

            Assert.That(result, Is.False);
        }

        [Test]
        public async Task StarCollectionAsync_ThrowsArgumentException_WhenCollectionNotFound()
        {
            List<Collection> collections = new List<Collection>();

            collectionRepositoryMock
                .Setup(r => r.All())
                .Returns(collections.AsQueryable().BuildMock());

            Assert.ThrowsAsync<ArgumentException>(
                async () => await likeService.StarCollectionAsync(999, "user-123"));
        }

        [Test]
        public async Task StarCollectionAsync_ThrowsInvalidOperation_WhenUserStarsOwnCollection()
        {
            string userId = "user-123";

            List<Collection> collections = new List<Collection>
            {
                new Collection { Id = 1, UserId = userId, Title = "My Collection" }
            };

            collectionRepositoryMock
                .Setup(r => r.All())
                .Returns(collections.AsQueryable().BuildMock());

            Assert.ThrowsAsync<InvalidOperationException>(
                async () => await likeService.StarCollectionAsync(1, userId));
        }

        [Test]
        public async Task StarCollectionAsync_ThrowsInvalidOperation_WhenAlreadyStarred()
        {
            string userId = "user-123";
            int collectionId = 1;

            List<Collection> collections = new List<Collection>
            {
                new Collection { Id = collectionId, UserId = "other-user", Title = "Other Collection" }
            };

            List<Like> likes = new List<Like>
            {
                new Like { UserId = userId, CollectionId = collectionId }
            };

            collectionRepositoryMock
                .Setup(r => r.All())
                .Returns(collections.AsQueryable().BuildMock());

            likeRepositoryMock
                .Setup(r => r.All())
                .Returns(likes.AsQueryable().BuildMock());

            Assert.ThrowsAsync<InvalidOperationException>(
                async () => await likeService.StarCollectionAsync(collectionId, userId));
        }

        [Test]
        public async Task StarCollectionAsync_AddsLikeAndIncrementsStars_WhenValid()
        {
            string userId = "user-123";
            int collectionId = 1;

            Collection collection = new Collection
            {
                Id = collectionId,
                UserId = "other-user",
                Title = "Other Collection",
                TotalStars = 0
            };

            List<Collection> collections = new List<Collection> { collection };
            List<Like> likes = new List<Like>();

            collectionRepositoryMock
                .Setup(r => r.All())
                .Returns(collections.AsQueryable().BuildMock());

            likeRepositoryMock
                .Setup(r => r.All())
                .Returns(likes.AsQueryable().BuildMock());

            likeRepositoryMock
                .Setup(r => r.AddAsync(It.IsAny<Like>()))
                .Returns(Task.CompletedTask);

            likeRepositoryMock
                .Setup(r => r.SaveChangesAsync())
                .Returns(Task.CompletedTask);

            await likeService.StarCollectionAsync(collectionId, userId);

            likeRepositoryMock.Verify(r => r.AddAsync(It.IsAny<Like>()), Times.Once);

            likeRepositoryMock.Verify(r => r.SaveChangesAsync(), Times.Once);

            Assert.That(collection.TotalStars, Is.EqualTo(1));
        }

        [Test]
        public async Task RemoveStarAsync_DoesNothing_WhenLikeNotFound()
        {
            string userId = "user-123";

            int collectionId = 1;

            List<Like> likes = new List<Like>();

            likeRepositoryMock
                .Setup(r => r.All())
                .Returns(likes.AsQueryable().BuildMock());

            await likeService.RemoveStarAsync(collectionId, userId);

            likeRepositoryMock.Verify(r => r.SaveChangesAsync(), Times.Never);
        }

        [Test]
        public async Task RemoveStarAsync_RemovesLikeAndDecrementsStars_WhenLikeExists()
        {
            string userId = "user-123";

            int collectionId = 1;

            Like like = new Like { UserId = userId, CollectionId = collectionId };

            dbContext.Set<Like>().Add(like);

            await dbContext.SaveChangesAsync();

            List<Like> likes = dbContext.Set<Like>().ToList();

            Collection collection = new Collection
            {
                Id = collectionId,
                UserId = "other-user",
                TotalStars = 3
            };

            likeRepositoryMock
                .Setup(r => r.All())
                .Returns(likes.AsQueryable().BuildMock());

            collectionRepositoryMock
                .Setup(r => r.GetByIdAsync(collectionId))
                .ReturnsAsync(collection);

            likeRepositoryMock
                .Setup(r => r.SaveChangesAsync())
                .Returns(Task.CompletedTask);

            await likeService.RemoveStarAsync(collectionId, userId);

            Assert.That(collection.TotalStars, Is.EqualTo(2));

            likeRepositoryMock.Verify(r => r.SaveChangesAsync(), Times.Once);
        }

        [Test]
        public async Task GetStarredCollectionsAsync_ReturnsEmpty_WhenUserHasNoStars()
        {
            string userId = "user-123";

            List<Like> likes = new List<Like>();

            likeRepositoryMock
                .Setup(r => r.All())
                .Returns(likes.AsQueryable().BuildMock());

            IEnumerable<CollectionAllViewModel> result = await likeService
                .GetStarredCollectionsAsync(userId);

            Assert.That(result, Is.Empty);
        }

        [Test]
        public async Task GetStarredCollectionsAsync_ReturnsCollections_WhenUserHasStars()
        {
            string userId = "user-123";

            ApplicationUser owner = new ApplicationUser
            {
                Id = "owner-456",
                UserName = "owner@test.com",
                AvatarUrl = "/images/avatar.jpg"
            };

            dbContext.Users.Add(owner);

            await dbContext.SaveChangesAsync();

            List<Like> likes = new List<Like>
            {
                new Like
                {
                    UserId = userId,
                    CollectionId = 1,
                    Collection = new Collection
                    {
                        Id = 1,
                        Title = "Awesome Collection",
                        TotalStars = 10,
                        UserId = "owner-456",
                        User = new ApplicationUser { UserName = "owner@test.com" },
                        Games = new List<Game>
                        {
                            new Game { ImageUrl = "img1.jpg" },
                            new Game { ImageUrl = "img2.jpg" },
                            new Game { ImageUrl = "img3.jpg" }
                        }
                    }
                }
            };

            likeRepositoryMock
                .Setup(r => r.All())
                .Returns(likes.AsQueryable().BuildMock());

            IEnumerable<CollectionAllViewModel> result = await likeService
                .GetStarredCollectionsAsync(userId);

            List<CollectionAllViewModel> resultList = result.ToList();

            Assert.That(resultList.Count, Is.EqualTo(1));

            Assert.That(resultList[0].Title, Is.EqualTo("Awesome Collection"));

            Assert.That(resultList[0].TotalStars, Is.EqualTo(10));

            Assert.That(resultList[0].Publisher, Is.EqualTo("owner"));

            Assert.That(resultList[0].GameImages.Count, Is.EqualTo(3));
        }
    }
}