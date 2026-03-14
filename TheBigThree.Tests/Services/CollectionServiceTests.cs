using MockQueryable.Moq;
using Moq;
using TheBigThree.Data.Models;
using TheBigThree.Services.Core.Repositories;
using TheBigThree.Web.ViewModels;

namespace TheBigThree.Tests.Services
{
    [TestFixture]
    public class CollectionServiceTests
    {
        private Mock<IRepository<Collection>> collectionRepositoryMock;
        private Mock<IRepository<Game>> gameRepositoryMock;
        private Mock<IRepository<Like>> likeRepositoryMock;
        private Mock<IRepository<Genre>> genreRepositoryMock;
        private TheBigThree.Services.CollectionService collectionService;

        [SetUp]
        public void SetUp()
        {
            collectionRepositoryMock = new Mock<IRepository<Collection>>();

            gameRepositoryMock = new Mock<IRepository<Game>>();

            likeRepositoryMock = new Mock<IRepository<Like>>();

            genreRepositoryMock = new Mock<IRepository<Genre>>();

            collectionService = new TheBigThree.Services.CollectionService(
                collectionRepositoryMock.Object,
                gameRepositoryMock.Object,
                likeRepositoryMock.Object,
                genreRepositoryMock.Object,
                null!);
        }

        [Test]
        public async Task UserHasCollectionAsync_ReturnsTrue_WhenUserHasCollection()
        {
            string userId = "user-123";

            List<Collection> collections = new List<Collection>
            {
                new Collection { Id = 1, UserId = userId, Title = "My Collection" }
            };

            collectionRepositoryMock
                .Setup(r => r.All())
                .Returns(collections.AsQueryable().BuildMock());

            bool result = await collectionService.UserHasCollectionAsync(userId);

            Assert.That(result, Is.True);
        }

        [Test]
        public async Task UserHasCollectionAsync_ReturnsFalse_WhenUserHasNoCollection()
        {
            string userId = "user-123";

            List<Collection> collections = new List<Collection>();

            collectionRepositoryMock
                .Setup(r => r.All())
                .Returns(collections.AsQueryable().BuildMock());

            bool result = await collectionService.UserHasCollectionAsync(userId);

            Assert.That(result, Is.False);
        }

        [Test]
        public async Task GetMineCollectionAsync_ReturnsNull_WhenUserHasNoCollection()
        {
            string userId = "user-123";

            List<Collection> collections = new List<Collection>();

            collectionRepositoryMock
                .Setup(r => r.All())
                .Returns(collections.AsQueryable().BuildMock());

            CollectionMineViewModel? result = await collectionService.GetMineCollectionAsync(userId);

            Assert.That(result, Is.Null);
        }

        [Test]
        public async Task GetMineCollectionAsync_ReturnsCollection_WhenUserHasOne()
        {
            string userId = "user-123";

            List<Collection> collections = new List<Collection>
            {
                new Collection
                {
                    Id = 1,
                    UserId = userId,
                    Title = "My Big Three",
                    TotalStars = 5,
                    Games = new List<Game>
                    {
                        new Game { Title = "Game1", ImageUrl = "url1", Description = "desc1", Genre = new Genre { Name = "Action" } },
                        new Game { Title = "Game2", ImageUrl = "url2", Description = "desc2", Genre = new Genre { Name = "RPG" } },
                        new Game { Title = "Game3", ImageUrl = "url3", Description = "desc3", Genre = new Genre { Name = "Horror" } }
                    }
                }
            };

            collectionRepositoryMock
                .Setup(r => r.All())
                .Returns(collections.AsQueryable().BuildMock());

            CollectionMineViewModel? result = await collectionService.GetMineCollectionAsync(userId);

            Assert.That(result, Is.Not.Null);

            Assert.That(result!.Title, Is.EqualTo("My Big Three"));

            Assert.That(result.TotalStars, Is.EqualTo(5));

            Assert.That(result.Games.Count, Is.EqualTo(3));
        }

        [Test]
        public async Task AddCollectionAsync_ThrowsException_WhenUserAlreadyHasCollection()
        {
            string userId = "user-123";
            List<Collection> collections = new List<Collection>
            {
                new Collection { Id = 1, UserId = userId, Title = "Existing" }
            };

            collectionRepositoryMock
                .Setup(r => r.All())
                .Returns(collections.AsQueryable().BuildMock());

            CollectionFormViewModel form = new CollectionFormViewModel
            {
                Title = "New Collection",
                Games = new List<GameFormViewModel>
                {
                    new GameFormViewModel { Title = "G1", Description = "D1", ImageUrl = "url1", GenreId = 1 },
                    new GameFormViewModel { Title = "G2", Description = "D2", ImageUrl = "url2", GenreId = 2 },
                    new GameFormViewModel { Title = "G3", Description = "D3", ImageUrl = "url3", GenreId = 3 }
                }
            };

            Assert.ThrowsAsync<InvalidOperationException>(
                async () => await collectionService.AddCollectionAsync(form, userId));
        }

        [Test]
        public async Task AddCollectionAsync_AddsCollection_WhenUserHasNoCollection()
        {
            string userId = "user-123";

            List<Collection> collections = new List<Collection>();

            collectionRepositoryMock
                .Setup(r => r.All())
                .Returns(collections.AsQueryable().BuildMock());

            collectionRepositoryMock
                .Setup(r => r.AddAsync(It.IsAny<Collection>()))
                .Returns(Task.CompletedTask);

            collectionRepositoryMock
                .Setup(r => r.SaveChangesAsync())
                .Returns(Task.CompletedTask);

            gameRepositoryMock
                .Setup(r => r.AddAsync(It.IsAny<Game>()))
                .Returns(Task.CompletedTask);

            CollectionFormViewModel form = new CollectionFormViewModel
            {
                Title = "New Collection",
                Games = new List<GameFormViewModel>
                {
                    new GameFormViewModel { Title = "G1", Description = "D1", ImageUrl = "url1", GenreId = 1 },
                    new GameFormViewModel { Title = "G2", Description = "D2", ImageUrl = "url2", GenreId = 2 },
                    new GameFormViewModel { Title = "G3", Description = "D3", ImageUrl = "url3", GenreId = 3 }
                }
            };

            await collectionService
                .AddCollectionAsync(form, userId);

            collectionRepositoryMock
                .Verify(r => r.AddAsync(It.IsAny<Collection>()), Times.Once);

            collectionRepositoryMock
                .Verify(r => r.SaveChangesAsync(), Times.Once);

            gameRepositoryMock
                .Verify(r => r.AddAsync(It.IsAny<Game>()), Times.Exactly(3));
        }

        [Test]
        public async Task EditCollectionAsync_ThrowsException_WhenCollectionNotFound()
        {
            string userId = "user-123";

            List<Collection> collections = new List<Collection>();

            collectionRepositoryMock
                .Setup(r => r.All())
                .Returns(collections.AsQueryable().BuildMock());

            CollectionFormViewModel form = new CollectionFormViewModel { Title = "Updated" };

            Assert.ThrowsAsync<ArgumentException>(
                async () => await collectionService.EditCollectionAsync(form, 999, userId));
        }

        [Test]
        public async Task GetCollectionForEditAsync_ReturnsNull_WhenCollectionNotFound()
        {
            string userId = "user-123";

            List<Collection> collections = new List<Collection>();

            collectionRepositoryMock
                .Setup(r => r.All())
                .Returns(collections.AsQueryable().BuildMock());

            CollectionFormViewModel? result = await collectionService.GetCollectionForEditAsync(999, userId);

            Assert.That(result, Is.Null);
        }

        [Test]
        public async Task GetCollectionForDeleteAsync_ReturnsNull_WhenCollectionNotFound()
        {
            string userId = "user-123";

            List<Collection> collections = new List<Collection>();

            collectionRepositoryMock
                .Setup(r => r.All())
                .Returns(collections.AsQueryable().BuildMock());

            CollectionDetailsViewModel? result = await collectionService
                .GetCollectionForDeleteAsync(999, userId);

            Assert.That(result, Is.Null);
        }

        [Test]
        public async Task DeleteCollectionAsync_DoesNothing_WhenCollectionNotFound()
        {
            string userId = "user-123";

            List<Collection> collections = new List<Collection>();

            collectionRepositoryMock
                .Setup(r => r.All())
                .Returns(collections.AsQueryable().BuildMock());

            await collectionService.DeleteCollectionAsync(999, userId);

            collectionRepositoryMock.Verify(r => r.DeleteAsync(It.IsAny<int>()), Times.Never);

            collectionRepositoryMock.Verify(r => r.SaveChangesAsync(), Times.Never);
        }
    }
}