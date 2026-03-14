using MockQueryable.Moq;
using Moq;
using TheBigThree.Data.Models;
using TheBigThree.Services.Core.Repositories;

namespace TheBigThree.Tests.Services
{
    [TestFixture]
    public class LikeServiceTests
    {
        private Mock<IRepository<Like>> likeRepositoryMock;
        private Mock<IRepository<Collection>> collectionRepositoryMock;
        private TheBigThree.Services.LikeService likeService;

        [SetUp]
        public void SetUp()
        {
            likeRepositoryMock = new Mock<IRepository<Like>>();

            collectionRepositoryMock = new Mock<IRepository<Collection>>();

            likeService = new TheBigThree.Services.LikeService(
                likeRepositoryMock.Object,
                collectionRepositoryMock.Object,
                null!);
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

            Collection collection = new Collection { Id = collectionId, UserId = "other-user", Title = "Other Collection", TotalStars = 0 };

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
        public async Task RemoveStarAsync_DoesNotDecrementBelowZero_WhenStarsAlreadyZero()
        {
            string userId = "user-123";

            int collectionId = 1;

            List<Like> likes = new List<Like>();

            likeRepositoryMock
                .Setup(r => r.All())
                .Returns(likes.AsQueryable().BuildMock());

            await likeService.RemoveStarAsync(collectionId, userId);

            collectionRepositoryMock.Verify(r => r.GetByIdAsync(It.IsAny<int>()), Times.Never);

            likeRepositoryMock.Verify(r => r.SaveChangesAsync(), Times.Never);
        }
    }
}