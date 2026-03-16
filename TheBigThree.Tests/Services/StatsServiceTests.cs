using Microsoft.AspNetCore.Identity;
using MockQueryable.Moq;
using Moq;
using TheBigThree.Data.Models;
using TheBigThree.Services.Core.Repositories;

namespace TheBigThree.Tests.Services
{
    [TestFixture]
    public class StatsServiceTests
    {
        private Mock<UserManager<ApplicationUser>> userManagerMock;
        private Mock<IRepository<Collection>> collectionRepositoryMock;
        private Mock<IRepository<Comment>> commentRepositoryMock;
        private Mock<IRepository<Game>> gameRepositoryMock;
        private TheBigThree.Services.StatsService statsService;

        [SetUp]
        public void SetUp()
        {
            var store = new Mock<IUserStore<ApplicationUser>>();
            userManagerMock = new Mock<UserManager<ApplicationUser>>(
                store.Object, null, null, null, null, null, null, null, null);

            collectionRepositoryMock = new Mock<IRepository<Collection>>();
            commentRepositoryMock = new Mock<IRepository<Comment>>();
            gameRepositoryMock = new Mock<IRepository<Game>>();

            statsService = new TheBigThree.Services.StatsService(
                userManagerMock.Object,
                collectionRepositoryMock.Object,
                commentRepositoryMock.Object,
                gameRepositoryMock.Object);
        }

        [Test]
        public async Task GetStatsAsync_ReturnsCorrectTotalCounts()
        {
            List<ApplicationUser> users = new List<ApplicationUser>
            {
                new ApplicationUser { Id = "u1", UserName = "User1" },
                new ApplicationUser { Id = "u2", UserName = "User2" },
                new ApplicationUser { Id = "u3", UserName = "User3" }
            };

            List<Collection> collections = new List<Collection>
            {
                new Collection { Id = 1, UserId = "u1", Title = "Col1", TotalStars = 10, User = new ApplicationUser { UserName = "User1" }, Comments = new List<Comment>() },
                new Collection { Id = 2, UserId = "u2", Title = "Col2", TotalStars = 5, User = new ApplicationUser { UserName = "User2" }, Comments = new List<Comment>() }
            };

            List<Comment> comments = new List<Comment>
            {
                new Comment { Id = 1, UserId = "u1", CollectionId = 1, Content = "Great!", User = new ApplicationUser { UserName = "User1" } },
                new Comment { Id = 2, UserId = "u2", CollectionId = 2, Content = "Awesome!", User = new ApplicationUser { UserName = "User2" } },
                new Comment { Id = 3, UserId = "u1", CollectionId = 1, Content = "Love it!", User = new ApplicationUser { UserName = "User1" } }
            };

            List<Game> games = new List<Game>();

            userManagerMock.Setup(u => u.Users).Returns(users.AsQueryable().BuildMock());

            collectionRepositoryMock.Setup(r => r.All()).Returns(collections.AsQueryable().BuildMock());

            commentRepositoryMock.Setup(r => r.All()).Returns(comments.AsQueryable().BuildMock());

            gameRepositoryMock.Setup(r => r.All()).Returns(games.AsQueryable().BuildMock());

            var result = await statsService.GetStatsAsync();

            Assert.That(result.TotalUsers, Is.EqualTo(3));

            Assert.That(result.TotalCollections, Is.EqualTo(2));

            Assert.That(result.TotalStars, Is.EqualTo(15));

            Assert.That(result.TotalComments, Is.EqualTo(3));
        }

        [Test]
        public async Task GetStatsAsync_ReturnsTopThreeCollections_OrderedByStars()
        {
            List<ApplicationUser> users = new List<ApplicationUser>
            {
                new ApplicationUser { Id = "u1", UserName = "User1" },
                new ApplicationUser { Id = "u2", UserName = "User2" },
                new ApplicationUser { Id = "u3", UserName = "User3" },
                new ApplicationUser { Id = "u4", UserName = "User4" }
            };

            List<Collection> collections = new List<Collection>
            {
                new Collection { Id = 1, UserId = "u1", Title = "Bronze", TotalStars = 5, User = new ApplicationUser { UserName = "User1" }, Comments = new List<Comment>() },
                new Collection { Id = 2, UserId = "u2", Title = "Gold", TotalStars = 20, User = new ApplicationUser { UserName = "User2" }, Comments = new List<Comment>() },
                new Collection { Id = 3, UserId = "u3", Title = "Silver", TotalStars = 10, User = new ApplicationUser { UserName = "User3" }, Comments = new List<Comment>() },
                new Collection { Id = 4, UserId = "u4", Title = "Last", TotalStars = 1, User = new ApplicationUser { UserName = "User4" }, Comments = new List<Comment>() }
            };

            List<Comment> comments = new List<Comment>();
            List<Game> games = new List<Game>();

            userManagerMock.Setup(u => u.Users)
                .Returns(users.AsQueryable().BuildMock());

            collectionRepositoryMock.Setup(r => r.All())
                .Returns(collections.AsQueryable().BuildMock());

            commentRepositoryMock.Setup(r => r.All())
                .Returns(comments.AsQueryable().BuildMock());

            gameRepositoryMock.Setup(r => r.All())
                .Returns(games.AsQueryable().BuildMock());

            var result = await statsService.GetStatsAsync();

            Assert.That(result.TopThreeCollections.Count, Is.EqualTo(3));

            Assert.That(result.TopThreeCollections[0].Title, Is.EqualTo("Gold"));

            Assert.That(result.TopThreeCollections[1].Title, Is.EqualTo("Silver"));

            Assert.That(result.TopThreeCollections[2].Title, Is.EqualTo("Bronze"));
        }

        [Test]
        public async Task GetStatsAsync_ReturnsTopGenres_OrderedByCount()
        {
            List<ApplicationUser> users = new List<ApplicationUser>();

            List<Collection> collections = new List<Collection>();

            List<Comment> comments = new List<Comment>();

            List<Game> games = new List<Game>
            {
                new Game { Id = 1, Title = "Game1", Genre = new Genre { Name = "RPG" } },
                new Game { Id = 2, Title = "Game2", Genre = new Genre { Name = "RPG" } },
                new Game { Id = 3, Title = "Game3", Genre = new Genre { Name = "Action" } },
                new Game { Id = 4, Title = "Game4", Genre = new Genre { Name = "RPG" } },
                new Game { Id = 5, Title = "Game5", Genre = new Genre { Name = "Action" } },
                new Game { Id = 6, Title = "Game6", Genre = new Genre { Name = "Horror" } }
            };

            userManagerMock.Setup(u => u.Users)
                .Returns(users.AsQueryable().BuildMock());

            collectionRepositoryMock.Setup(r => r.All())
                .Returns(collections.AsQueryable().BuildMock());

            commentRepositoryMock.Setup(r => r.All())
                .Returns(comments.AsQueryable().BuildMock());

            gameRepositoryMock.Setup(r => r.All())
                .Returns(games.AsQueryable().BuildMock());

            var result = await statsService.GetStatsAsync();

            Assert.That(result.TopGenres[0].Genre, Is.EqualTo("RPG"));
            Assert.That(result.TopGenres[0].Count, Is.EqualTo(3));
            Assert.That(result.TopGenres[1].Genre, Is.EqualTo("Action"));
            Assert.That(result.TopGenres[1].Count, Is.EqualTo(2));
        }

        [Test]
        public async Task GetLeaderboardAsync_ReturnsTopCollectors_OrderedByStars()
        {
            List<Collection> collections = new List<Collection>
            {
                new Collection { Id = 1, UserId = "u1", Title = "Col1", TotalStars = 5, User = new ApplicationUser { UserName = "User1" } },
                new Collection { Id = 2, UserId = "u2", Title = "Col2", TotalStars = 20, User = new ApplicationUser { UserName = "User2" } },
                new Collection { Id = 3, UserId = "u3", Title = "Col3", TotalStars = 10, User = new ApplicationUser { UserName = "User3" } }
            };

            List<Comment> comments = new List<Comment>();

            collectionRepositoryMock.Setup(r => r.All())
                .Returns(collections.AsQueryable().BuildMock());

            commentRepositoryMock.Setup(r => r.All())
                .Returns(comments.AsQueryable().BuildMock());

            var result = await statsService.GetLeaderboardAsync();

            Assert.That(result.TopCollectors[0].Username, Is.EqualTo("User2"));

            Assert.That(result.TopCollectors[0].Score, Is.EqualTo(20));

            Assert.That(result.TopCollectors[1].Username, Is.EqualTo("User3"));

            Assert.That(result.TopCollectors[2].Username, Is.EqualTo("User1"));
        }

        [Test]
        public async Task GetLeaderboardAsync_AssignsCorrectPositions()
        {
            List<Collection> collections = new List<Collection>
            {
                new Collection { Id = 1, UserId = "u1", Title = "Col1", TotalStars = 15, User = new ApplicationUser { UserName = "User1" } },
                new Collection { Id = 2, UserId = "u2", Title = "Col2", TotalStars = 10, User = new ApplicationUser { UserName = "User2" } },
                new Collection { Id = 3, UserId = "u3", Title = "Col3", TotalStars = 5, User = new ApplicationUser { UserName = "User3" } }
            };

            List<Comment> comments = new List<Comment>();

            collectionRepositoryMock.Setup(r => r.All())
                .Returns(collections.AsQueryable().BuildMock());

            commentRepositoryMock.Setup(r => r.All())
                .Returns(comments.AsQueryable().BuildMock());

            var result = await statsService.GetLeaderboardAsync();

            Assert.That(result.TopCollectors[0].Position, Is.EqualTo(1));

            Assert.That(result.TopCollectors[1].Position, Is.EqualTo(2));

            Assert.That(result.TopCollectors[2].Position, Is.EqualTo(3));
        }

        [Test]
        public async Task GetLeaderboardAsync_AssignsCorrectRank_BasedOnStars()
        {
            List<Collection> collections = new List<Collection>
            {
                new Collection { Id = 1, UserId = "u1", Title = "Col1", TotalStars = 100, User = new ApplicationUser { UserName = "LegendaryUser" } }
            };

            List<Comment> comments = new List<Comment>();

            collectionRepositoryMock.Setup(r => r.All())
                .Returns(collections.AsQueryable().BuildMock());

            commentRepositoryMock.Setup(r => r.All())
                .Returns(comments.AsQueryable().BuildMock());

            var result = await statsService.GetLeaderboardAsync();

            Assert.That(result.TopCollectors[0].Rank, Is.EqualTo("Legendary Collector"));
        }

        [Test]
        public async Task GetLeaderboardAsync_ReturnsTopCommenters_OrderedByCommentCount()
        {
            List<Collection> collections = new List<Collection>();

            List<Comment> comments = new List<Comment>
            {
                new Comment { Id = 1, UserId = "u1", Content = "c1", User = new ApplicationUser { UserName = "User1" } },
                new Comment { Id = 2, UserId = "u1", Content = "c2", User = new ApplicationUser { UserName = "User1" } },
                new Comment { Id = 3, UserId = "u1", Content = "c3", User = new ApplicationUser { UserName = "User1" } },
                new Comment { Id = 4, UserId = "u2", Content = "c4", User = new ApplicationUser { UserName = "User2" } },
                new Comment { Id = 5, UserId = "u2", Content = "c5", User = new ApplicationUser { UserName = "User2" } }
            };

            collectionRepositoryMock.Setup(r => r.All())
                .Returns(collections.AsQueryable().BuildMock());

            commentRepositoryMock.Setup(r => r.All())
                .Returns(comments.AsQueryable().BuildMock());

            var result = await statsService.GetLeaderboardAsync();

            Assert.That(result.TopCommenters[0].Username, Is.EqualTo("User1"));

            Assert.That(result.TopCommenters[0].Score, Is.EqualTo(3));

            Assert.That(result.TopCommenters[1].Username, Is.EqualTo("User2"));

            Assert.That(result.TopCommenters[1].Score, Is.EqualTo(2));
        }
    }
}