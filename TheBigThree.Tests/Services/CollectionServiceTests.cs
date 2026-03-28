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
    public class CollectionServiceTests
    {
        private Mock<IRepository<Collection>> collectionRepositoryMock;
        private Mock<IRepository<Game>> gameRepositoryMock;
        private Mock<IRepository<Like>> likeRepositoryMock;
        private Mock<IRepository<Genre>> genreRepositoryMock;
        private TheBigThreeDbContext dbContext;
        private TheBigThree.Services.CollectionService collectionService;

        [SetUp]
        public void SetUp()
        {
            collectionRepositoryMock = new Mock<IRepository<Collection>>();
            gameRepositoryMock = new Mock<IRepository<Game>>();
            likeRepositoryMock = new Mock<IRepository<Like>>();
            genreRepositoryMock = new Mock<IRepository<Genre>>();

            DbContextOptions<TheBigThreeDbContext> options = new DbContextOptionsBuilder<TheBigThreeDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            dbContext = new TheBigThreeDbContext(options);

            collectionService = new TheBigThree.Services.CollectionService(
                collectionRepositoryMock.Object,
                gameRepositoryMock.Object,
                likeRepositoryMock.Object,
                genreRepositoryMock.Object,
                dbContext);
        }

        [TearDown]
        public void TearDown()
        {
            dbContext.Dispose();
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

            await collectionService.AddCollectionAsync(form, userId);

            collectionRepositoryMock.Verify(r => r.AddAsync(It.IsAny<Collection>()), Times.Once);

            collectionRepositoryMock.Verify(r => r.SaveChangesAsync(), Times.Once);

            gameRepositoryMock.Verify(r => r.AddAsync(It.IsAny<Game>()), Times.Exactly(3));
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
        public async Task EditCollectionAsync_UpdatesTitleAndGames_WhenCollectionFound()
        {
            string userId = "user-123";

            List<Collection> collections = new List<Collection>
            {
                new Collection
                {
                    Id = 1,
                    UserId = userId,
                    Title = "Old Title",
                    Games = new List<Game>
                    {
                        new Game { Id = 10, Title = "Old Game 1", Description = "Old Desc 1", ImageUrl = "old1.jpg", GenreId = 1 },
                        new Game { Id = 11, Title = "Old Game 2", Description = "Old Desc 2", ImageUrl = "old2.jpg", GenreId = 2 },
                        new Game { Id = 12, Title = "Old Game 3", Description = "Old Desc 3", ImageUrl = "old3.jpg", GenreId = 3 }
                    }
                }
            };

            collectionRepositoryMock
                .Setup(r => r.All())
                .Returns(collections.AsQueryable().BuildMock());

            collectionRepositoryMock
                .Setup(r => r.SaveChangesAsync())
                .Returns(Task.CompletedTask);

            CollectionFormViewModel form = new CollectionFormViewModel
            {
                Title = "New Title",
                Games = new List<GameFormViewModel>
                {
                    new GameFormViewModel { Title = "New Game 1", Description = "New Desc 1", ImageUrl = "new1.jpg", GenreId = 5 },
                    new GameFormViewModel { Title = "New Game 2", Description = "New Desc 2", ImageUrl = "new2.jpg", GenreId = 6 },
                    new GameFormViewModel { Title = "New Game 3", Description = "New Desc 3", ImageUrl = "new3.jpg", GenreId = 7 }
                }
            };

            await collectionService.EditCollectionAsync(form, 1, userId);

            Collection updatedCollection = collections.First();

            Assert.That(updatedCollection.Title, Is.EqualTo("New Title"));

            Assert.That(updatedCollection.Games.ElementAt(0).Title, Is.EqualTo("New Game 1"));

            Assert.That(updatedCollection.Games.ElementAt(1).GenreId, Is.EqualTo(6));

            collectionRepositoryMock.Verify(r => r.SaveChangesAsync(), Times.Once);
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
        public async Task GetCollectionForEditAsync_ReturnsPopulatedForm_WhenCollectionFound()
        {
            string userId = "user-123";

            List<Genre> genres = new List<Genre>
            {
                new Genre { Id = 1, Name = "Action" },
                new Genre { Id = 2, Name = "RPG" }
            };

            List<Collection> collections = new List<Collection>
            {
                new Collection
                {
                    Id = 1,
                    UserId = userId,
                    Title = "My Big Three",
                    Games = new List<Game>
                    {
                        new Game { Title = "Game1", ImageUrl = "img1.jpg", Description = "Desc1", GenreId = 1 },
                        new Game { Title = "Game2", ImageUrl = "img2.jpg", Description = "Desc2", GenreId = 2 },
                        new Game { Title = "Game3", ImageUrl = "img3.jpg", Description = "Desc3", GenreId = 1 }
                    }
                }
            };

            collectionRepositoryMock
                .Setup(r => r.All())
                .Returns(collections.AsQueryable().BuildMock());

            genreRepositoryMock
                .Setup(r => r.All())
                .Returns(genres.AsQueryable().BuildMock());

            CollectionFormViewModel? result = await collectionService.GetCollectionForEditAsync(1, userId);

            Assert.That(result, Is.Not.Null);

            Assert.That(result!.Title, Is.EqualTo("My Big Three"));

            Assert.That(result.Games.Count, Is.EqualTo(3));

            Assert.That(result.Games[0].Title, Is.EqualTo("Game1"));

            Assert.That(result.Games[0].Genres.Count, Is.EqualTo(2));
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

        [Test]
        public async Task DeleteCollectionAsync_DeletesGamesAndCollection_WhenFound()
        {
            string userId = "user-123";

            Like like = new Like { UserId = "other-user", CollectionId = 1 };

            dbContext.Set<Like>().Add(like);

            await dbContext.SaveChangesAsync();

            List<Collection> collections = new List<Collection>
            {
                new Collection
                {
                    Id = 1,
                    UserId = userId,
                    Title = "To Delete",
                    Games = new List<Game>
                    {
                        new Game { Id = 10, Title = "Game1" },
                        new Game { Id = 11, Title = "Game2" },
                        new Game { Id = 12, Title = "Game3" }
                    }
                }
            };

            List<Like> likes = dbContext.Set<Like>().ToList();

            collectionRepositoryMock
                .Setup(r => r.All())
                .Returns(collections.AsQueryable().BuildMock());

            likeRepositoryMock
                .Setup(r => r.All())
                .Returns(likes.AsQueryable().BuildMock());

            gameRepositoryMock
                .Setup(r => r.DeleteAsync(It.IsAny<int>()))
                .Returns(Task.CompletedTask);

            collectionRepositoryMock
                .Setup(r => r.DeleteAsync(1))
                .Returns(Task.CompletedTask);

            collectionRepositoryMock
                .Setup(r => r.SaveChangesAsync())
                .Returns(Task.CompletedTask);

            await collectionService.DeleteCollectionAsync(1, userId);

            gameRepositoryMock.Verify(r => r.DeleteAsync(It.IsAny<int>()), Times.Exactly(3));

            collectionRepositoryMock.Verify(r => r.DeleteAsync(1), Times.Once);

            collectionRepositoryMock.Verify(r => r.SaveChangesAsync(), Times.Once);
        }

        [Test]
        public async Task GetNewAddFormModelAsync_ReturnsFormWithThreeGamesAndGenres()
        {
            List<Genre> genres = new List<Genre>
            {
                new Genre { Id = 1, Name = "Action" },
                new Genre { Id = 2, Name = "RPG" },
                new Genre { Id = 3, Name = "Horror" }
            };

            genreRepositoryMock
                .Setup(r => r.All())
                .Returns(genres.AsQueryable().BuildMock());

            CollectionFormViewModel result = await collectionService.GetNewAddFormModelAsync();

            Assert.That(result, Is.Not.Null);

            Assert.That(result.Games.Count, Is.EqualTo(3));

            Assert.That(result.Games[0].Genres.Count, Is.EqualTo(3));

            Assert.That(result.Games[1].Genres.First().Name, Is.EqualTo("Action"));
        }

        [Test]
        public async Task GetCollectionDetailsByIdAsync_ReturnsNull_WhenCollectionNotFound()
        {
            List<Collection> collections = new List<Collection>();

            collectionRepositoryMock
                .Setup(r => r.All())
                .Returns(collections.AsQueryable().BuildMock());

            CollectionDetailsViewModel? result = await collectionService
                .GetCollectionDetailsByIdAsync(999);

            Assert.That(result, Is.Null);
        }

        [Test]
        public async Task GetCollectionDetailsByIdAsync_ReturnsDetails_WhenCollectionFound()
        {
            ApplicationUser owner = new ApplicationUser
            {
                Id = "owner-456",
                UserName = "owner@test.com",
                AvatarUrl = "/images/avatar.jpg"
            };

            dbContext.Users.Add(owner);

            await dbContext.SaveChangesAsync();

            List<Collection> collections = new List<Collection>
            {
                new Collection
                {
                    Id = 1,
                    Title = "My Big Three",
                    TotalStars = 7,
                    UserId = "owner-456",
                    User = new ApplicationUser { UserName = "owner@test.com" },
                    Games = new List<Game>
                    {
                        new Game { Title = "Game1", Description = "Desc1", ImageUrl = "img1.jpg", Genre = new Genre { Name = "Action" } },
                        new Game { Title = "Game2", Description = "Desc2", ImageUrl = "img2.jpg", Genre = new Genre { Name = "RPG" } },
                        new Game { Title = "Game3", Description = "Desc3", ImageUrl = "img3.jpg", Genre = new Genre { Name = "Horror" } }
                    }
                }
            };

            collectionRepositoryMock
                .Setup(r => r.All())
                .Returns(collections.AsQueryable().BuildMock());

            CollectionDetailsViewModel? result = await collectionService
                .GetCollectionDetailsByIdAsync(1);

            Assert.That(result, Is.Not.Null);

            Assert.That(result!.Title, Is.EqualTo("My Big Three"));

            Assert.That(result.TotalStars, Is.EqualTo(7));

            Assert.That(result.Publisher, Is.EqualTo("owner"));

            Assert.That(result.Games.Count, Is.EqualTo(3));

            Assert.That(result.AvatarUrl, Is.EqualTo("/images/avatar.jpg"));
        }

        [Test]
        public async Task GetAllCollectionsAsync_ReturnsAllCollections_WhenNoFilters()
        {
            ApplicationUser owner = new ApplicationUser
            {
                Id = "owner-456",
                UserName = "owner@test.com",
                AvatarUrl = null
            };

            dbContext.Users.Add(owner);

            await dbContext.SaveChangesAsync();

            List<Genre> genres = new List<Genre>
            {
                new Genre { Id = 1, Name = "Action" }
            };

            List<Collection> collections = new List<Collection>
            {
                new Collection
                {
                    Id = 1,
                    Title = "Collection One",
                    TotalStars = 5,
                    UserId = "owner-456",
                    CreatedOn = new DateTime(2024, 1, 1),
                    User = new ApplicationUser { UserName = "owner@test.com" },
                    Games = new List<Game> { new Game { ImageUrl = "img1.jpg" } }
                },
                new Collection
                {
                    Id = 2,
                    Title = "Collection Two",
                    TotalStars = 10,
                    UserId = "owner-456",
                    CreatedOn = new DateTime(2024, 6, 1),
                    User = new ApplicationUser { UserName = "owner@test.com" },
                    Games = new List<Game> { new Game { ImageUrl = "img2.jpg" } }
                }
            };

            collectionRepositoryMock
                .Setup(r => r.All())
                .Returns(collections.AsQueryable().BuildMock());

            genreRepositoryMock
                .Setup(r => r.All())
                .Returns(genres.AsQueryable().BuildMock());

            CollectionQueryModel query = new CollectionQueryModel
            {
                CurrentPage = 1,
                CollectionsPerPage = 10
            };

            CollectionQueryModel result = await collectionService.GetAllCollectionsAsync(query);

            Assert.That(result.TotalCollections, Is.EqualTo(2));

            Assert.That(result.Collections.Count, Is.EqualTo(2));

            Assert.That(result.Genres.Count, Is.EqualTo(1));
        }

        [Test]
        public async Task GetAllCollectionsAsync_FiltersCollections_BySearchTerm()
        {
            ApplicationUser owner = new ApplicationUser
            {
                Id = "owner-456",
                UserName = "owner@test.com",
                AvatarUrl = null
            };

            dbContext.Users.Add(owner);

            await dbContext.SaveChangesAsync();

            List<Genre> genres = new List<Genre>();

            List<Collection> collections = new List<Collection>
            {
                new Collection
                {
                    Id = 1,
                    Title = "Zelda Legends",
                    TotalStars = 5,
                    UserId = "owner-456",
                    CreatedOn = new DateTime(2024, 1, 1),
                    User = new ApplicationUser { UserName = "owner@test.com" },
                    Games = new List<Game> { new Game { Title = "Zelda", ImageUrl = "img1.jpg" } }
                },
                new Collection
                {
                    Id = 2,
                    Title = "Shooter Picks",
                    TotalStars = 3,
                    UserId = "owner-456",
                    CreatedOn = new DateTime(2024, 2, 1),
                    User = new ApplicationUser { UserName = "owner@test.com" },
                    Games = new List<Game> { new Game { Title = "Halo", ImageUrl = "img2.jpg" } }
                }
            };

            collectionRepositoryMock
                .Setup(r => r.All())
                .Returns(collections.AsQueryable().BuildMock());

            genreRepositoryMock
                .Setup(r => r.All())
                .Returns(genres.AsQueryable().BuildMock());

            CollectionQueryModel query = new CollectionQueryModel
            {
                CurrentPage = 1,
                CollectionsPerPage = 10,
                SearchTerm = "zelda"
            };

            CollectionQueryModel result = await collectionService.GetAllCollectionsAsync(query);

            Assert.That(result.TotalCollections, Is.EqualTo(1));

            Assert.That(result.Collections.First().Title, Is.EqualTo("Zelda Legends"));
        }

        [Test]
        public async Task GetAllCollectionsAsync_SortsByStars_WhenSortingIsStars()
        {
            ApplicationUser owner = new ApplicationUser
            {
                Id = "owner-456",
                UserName = "owner@test.com",
                AvatarUrl = null
            };

            dbContext.Users.Add(owner);

            await dbContext.SaveChangesAsync();

            List<Genre> genres = new List<Genre>();

            List<Collection> collections = new List<Collection>
            {
                new Collection
                {
                    Id = 1,
                    Title = "Low Stars",
                    TotalStars = 2,
                    UserId = "owner-456",
                    CreatedOn = new DateTime(2024, 1, 1),
                    User = new ApplicationUser { UserName = "owner@test.com" },
                    Games = new List<Game>()
                },
                new Collection
                {
                    Id = 2,
                    Title = "High Stars",
                    TotalStars = 99,
                    UserId = "owner-456",
                    CreatedOn = new DateTime(2024, 2, 1),
                    User = new ApplicationUser { UserName = "owner@test.com" },
                    Games = new List<Game>()
                }
            };

            collectionRepositoryMock
                .Setup(r => r.All())
                .Returns(collections.AsQueryable().BuildMock());

            genreRepositoryMock
                .Setup(r => r.All())
                .Returns(genres.AsQueryable().BuildMock());

            CollectionQueryModel query = new CollectionQueryModel
            {
                CurrentPage = 1,
                CollectionsPerPage = 10,
                Sorting = "Stars"
            };

            CollectionQueryModel result = await collectionService.GetAllCollectionsAsync(query);

            Assert.That(result.Collections.First().Title, Is.EqualTo("High Stars"));

            Assert.That(result.Collections.Last().Title, Is.EqualTo("Low Stars"));
        }
    }
}