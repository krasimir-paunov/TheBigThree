using Microsoft.AspNetCore.Identity;
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
    public class AdminServiceTests
    {
        private Mock<UserManager<ApplicationUser>> userManagerMock;
        private Mock<IRepository<Collection>> collectionRepositoryMock;
        private Mock<IRepository<Comment>> commentRepositoryMock;
        private TheBigThreeDbContext dbContext;
        private TheBigThree.Services.AdminService adminService;

        [SetUp]
        public void SetUp()
        {
            Mock<IUserStore<ApplicationUser>> userStoreMock = new Mock<IUserStore<ApplicationUser>>();

            userManagerMock = new Mock<UserManager<ApplicationUser>>(
                userStoreMock.Object,
                null!, null!, null!, null!, null!, null!, null!, null!);

            collectionRepositoryMock = new Mock<IRepository<Collection>>();

            commentRepositoryMock = new Mock<IRepository<Comment>>();

            DbContextOptions<TheBigThreeDbContext> options = new DbContextOptionsBuilder<TheBigThreeDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            dbContext = new TheBigThreeDbContext(options);

            adminService = new TheBigThree.Services.AdminService(
                userManagerMock.Object,
                collectionRepositoryMock.Object,
                commentRepositoryMock.Object,
                dbContext);
        }

        [TearDown]
        public void TearDown()
        {
            dbContext.Dispose();
        }

        [Test]
        public async Task DeleteCollectionAsync_DoesNothing_WhenCollectionNotFound()
        {
            List<Collection> collections = new List<Collection>();

            collectionRepositoryMock
                .Setup(r => r.All())
                .Returns(collections.AsQueryable().BuildMock());

            await adminService.DeleteCollectionAsync(999);

            collectionRepositoryMock.Verify(r => r.DeleteAsync(It.IsAny<int>()), Times.Never);

            collectionRepositoryMock.Verify(r => r.SaveChangesAsync(), Times.Never);
        }

        [Test]
        public async Task DeleteCollectionAsync_DeletesAndSaves_WhenCollectionFound()
        {
            List<Collection> collections = new List<Collection>
            {
                new Collection { Id = 1, Title = "Test Collection", UserId = "user-123" }
            };

            collectionRepositoryMock
                .Setup(r => r.All())
                .Returns(collections.AsQueryable().BuildMock());

            collectionRepositoryMock
                .Setup(r => r.DeleteAsync(1))
                .Returns(Task.CompletedTask);

            collectionRepositoryMock
                .Setup(r => r.SaveChangesAsync())
                .Returns(Task.CompletedTask);

            await adminService.DeleteCollectionAsync(1);

            collectionRepositoryMock.Verify(r => r.DeleteAsync(1), Times.Once);

            collectionRepositoryMock.Verify(r => r.SaveChangesAsync(), Times.Once);
        }

        [Test]
        public async Task DeleteUserAsync_DoesNothing_WhenUserNotFound()
        {
            userManagerMock
                .Setup(u => u.FindByIdAsync("nonexistent-id"))
                .ReturnsAsync((ApplicationUser?)null);

            await adminService.DeleteUserAsync("nonexistent-id");

            userManagerMock.Verify(u => u.DeleteAsync(It.IsAny<ApplicationUser>()), Times.Never);
        }

        [Test]
        public async Task DeleteUserAsync_DeletesUser_WhenUserFound()
        {
            ApplicationUser user = new ApplicationUser { Id = "user-123", UserName = "TestUser" };

            userManagerMock
                .Setup(u => u.FindByIdAsync("user-123"))
                .ReturnsAsync(user);

            userManagerMock
                .Setup(u => u.DeleteAsync(user))
                .ReturnsAsync(IdentityResult.Success);

            await adminService.DeleteUserAsync("user-123");

            userManagerMock.Verify(u => u.DeleteAsync(user), Times.Once);
        }

        [Test]
        public async Task GetAllCollectionsAsync_ReturnsEmpty_WhenNoCollections()
        {
            List<Collection> collections = new List<Collection>();

            collectionRepositoryMock
                .Setup(r => r.All())
                .Returns(collections.AsQueryable().BuildMock());

            IEnumerable<CollectionManagementViewModel> result = await adminService.GetAllCollectionsAsync();

            Assert.That(result, Is.Empty);
        }

        [Test]
        public async Task GetAllCollectionsAsync_ReturnsCollections_OrderedByStarsDescending()
        {
            List<Collection> collections = new List<Collection>
            {
                new Collection
                {
                    Id = 1,
                    Title = "Low Stars",
                    TotalStars = 2,
                    CreatedOn = DateTime.UtcNow,
                    User = new ApplicationUser { UserName = "user1@test.com" },
                    Comments = new List<Comment>()
                },
                new Collection
                {
                    Id = 2,
                    Title = "High Stars",
                    TotalStars = 20,
                    CreatedOn = DateTime.UtcNow,
                    User = new ApplicationUser { UserName = "user2@test.com" },
                    Comments = new List<Comment>()
                }
            };

            collectionRepositoryMock
                .Setup(r => r.All())
                .Returns(collections.AsQueryable().BuildMock());

            IEnumerable<CollectionManagementViewModel> result = await adminService.GetAllCollectionsAsync();

            List<CollectionManagementViewModel> resultList = result.ToList();

            Assert.That(resultList.Count, Is.EqualTo(2));

            Assert.That(resultList[0].Title, Is.EqualTo("High Stars"));

            Assert.That(resultList[1].Title, Is.EqualTo("Low Stars"));
        }

        [Test]
        public async Task GetDashboardDataAsync_ReturnsCorrectTotals()
        {
            List<Collection> collections = new List<Collection>
            {
                new Collection
                {
                    Id = 1,
                    Title = "Top Collection",
                    TotalStars = 10,
                    CreatedOn = DateTime.UtcNow,
                    UserId = "user-123",
                    User = new ApplicationUser { UserName = "TestUser" },
                    Comments = new List<Comment>()
                }
            };

            List<Comment> comments = new List<Comment>
            {
                new Comment
                {
                    Id = 1,
                    Content = "Great!",
                    UserId = "user-123",
                    User = new ApplicationUser { UserName = "TestUser" }
                }
            };

            List<ApplicationUser> users = new List<ApplicationUser>
            {
                new ApplicationUser { Id = "user-123", UserName = "TestUser" }
            };

            collectionRepositoryMock
                .Setup(r => r.All())
                .Returns(collections.AsQueryable().BuildMock());

            commentRepositoryMock
                .Setup(r => r.All())
                .Returns(comments.AsQueryable().BuildMock());

            userManagerMock
                .Setup(u => u.Users)
                .Returns(users.AsQueryable().BuildMock());

            AdminDashboardViewModel result = await adminService.GetDashboardDataAsync();

            Assert.That(result.TotalCollections, Is.EqualTo(1));

            Assert.That(result.TotalComments, Is.EqualTo(1));

            Assert.That(result.TotalStars, Is.EqualTo(10));

            Assert.That(result.TotalUsers, Is.EqualTo(1));

            Assert.That(result.TopCollectionTitle, Is.EqualTo("Top Collection"));
        }

        [Test]
        public async Task PromoteToAdminAsync_DoesNothing_WhenUserNotFound()
        {
            userManagerMock
                .Setup(u => u.FindByIdAsync("nonexistent-id"))
                .ReturnsAsync((ApplicationUser?)null);

            await adminService.PromoteToAdminAsync("nonexistent-id");

            userManagerMock.Verify(u => u.AddToRoleAsync(
                It.IsAny<ApplicationUser>(),
                It.IsAny<string>()), Times.Never);
        }

        [Test]
        public async Task PromoteToAdminAsync_AddsAdminRole_WhenUserFound()
        {
            ApplicationUser user = new ApplicationUser { Id = "user-123", UserName = "TestUser" };

            userManagerMock
                .Setup(u => u.FindByIdAsync("user-123"))
                .ReturnsAsync(user);

            userManagerMock
                .Setup(u => u.AddToRoleAsync(user, "Administrator"))
                .ReturnsAsync(IdentityResult.Success);

            await adminService.PromoteToAdminAsync("user-123");

            userManagerMock.Verify(u => u.AddToRoleAsync(user, "Administrator"), Times.Once);
        }

        [Test]
        public async Task DemoteFromAdminAsync_DoesNothing_WhenUserNotFound()
        {
            userManagerMock
                .Setup(u => u.FindByIdAsync("nonexistent-id"))
                .ReturnsAsync((ApplicationUser?)null);

            await adminService.DemoteFromAdminAsync("nonexistent-id");

            userManagerMock.Verify(u => u.RemoveFromRoleAsync(
                It.IsAny<ApplicationUser>(),
                It.IsAny<string>()), Times.Never);
        }

        [Test]
        public async Task DemoteFromAdminAsync_RemovesAdminRole_WhenUserFound()
        {
            ApplicationUser user = new ApplicationUser { Id = "user-123", UserName = "TestUser" };

            userManagerMock
                .Setup(u => u.FindByIdAsync("user-123"))
                .ReturnsAsync(user);

            userManagerMock
                .Setup(u => u.RemoveFromRoleAsync(user, "Administrator"))
                .ReturnsAsync(IdentityResult.Success);

            await adminService.DemoteFromAdminAsync("user-123");

            userManagerMock.Verify(u => u.RemoveFromRoleAsync(user, "Administrator"), Times.Once);
        }
    }
}