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
    public class CommentServiceTests
    {
        private Mock<IRepository<Comment>> commentRepositoryMock;
        private TheBigThreeDbContext dbContext;
        private TheBigThree.Services.CommentService commentService;

        [SetUp]
        public void SetUp()
        {
            commentRepositoryMock = new Mock<IRepository<Comment>>();

            DbContextOptions<TheBigThreeDbContext> options = new DbContextOptionsBuilder<TheBigThreeDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            dbContext = new TheBigThreeDbContext(options);

            commentService = new TheBigThree.Services.CommentService(
                commentRepositoryMock.Object,
                dbContext);
        }

        [TearDown]
        public void TearDown()
        {
            dbContext.Dispose();
        }

        [Test]
        public async Task AddCommentAsync_AddsAndSaves_Successfully()
        {
            commentRepositoryMock
                .Setup(r => r.AddAsync(It.IsAny<Comment>()))
                .Returns(Task.CompletedTask);

            commentRepositoryMock
                .Setup(r => r.SaveChangesAsync())
                .Returns(Task.CompletedTask);

            AddCommentViewModel model = new AddCommentViewModel { Content = "Great collection!" };

            await commentService.AddCommentAsync(model, 1, "user-123");

            commentRepositoryMock.Verify(r => r.AddAsync(It.IsAny<Comment>()), Times.Once);

            commentRepositoryMock.Verify(r => r.SaveChangesAsync(), Times.Once);
        }

        [Test]
        public async Task AddCommentAsync_CreatesComment_WithCorrectProperties()
        {
            Comment? capturedComment = null;

            commentRepositoryMock
                .Setup(r => r.AddAsync(It.IsAny<Comment>()))
                .Callback<Comment>(c => capturedComment = c)
                .Returns(Task.CompletedTask);

            commentRepositoryMock
                .Setup(r => r.SaveChangesAsync())
                .Returns(Task.CompletedTask);

            AddCommentViewModel model = new AddCommentViewModel { Content = "Amazing!" };

            await commentService.AddCommentAsync(model, 5, "user-456");

            Assert.That(capturedComment, Is.Not.Null);

            Assert.That(capturedComment!.Content, Is.EqualTo("Amazing!"));

            Assert.That(capturedComment.CollectionId, Is.EqualTo(5));

            Assert.That(capturedComment.UserId, Is.EqualTo("user-456"));
        }

        [Test]
        public async Task DeleteCommentAsync_DeletesAndSaves_WhenUserOwnsComment()
        {
            string userId = "user-123";

            Comment comment = new Comment { Id = 1, UserId = userId, Content = "Test" };

            commentRepositoryMock
                .Setup(r => r.GetByIdAsync(1))
                .ReturnsAsync(comment);

            commentRepositoryMock
                .Setup(r => r.DeleteAsync(1))
                .Returns(Task.CompletedTask);

            commentRepositoryMock
                .Setup(r => r.SaveChangesAsync())
                .Returns(Task.CompletedTask);

            await commentService.DeleteCommentAsync(1, userId);

            commentRepositoryMock.Verify(r => r.DeleteAsync(1), Times.Once);

            commentRepositoryMock.Verify(r => r.SaveChangesAsync(), Times.Once);
        }

        [Test]
        public async Task DeleteCommentAsync_ThrowsUnauthorized_WhenUserDoesNotOwnComment()
        {
            Comment comment = new Comment { Id = 1, UserId = "other-user", Content = "Test" };

            commentRepositoryMock
                .Setup(r => r.GetByIdAsync(1))
                .ReturnsAsync(comment);

            Assert.ThrowsAsync<UnauthorizedAccessException>(
                async () => await commentService.DeleteCommentAsync(1, "user-123"));
        }

        [Test]
        public async Task DeleteCommentAsync_ThrowsArgumentException_WhenCommentNotFound()
        {
            commentRepositoryMock
                .Setup(r => r.GetByIdAsync(999))
                .ReturnsAsync((Comment?)null);

            Assert.ThrowsAsync<ArgumentException>(
                async () => await commentService.DeleteCommentAsync(999, "user-123"));
        }

        [Test]
        public async Task DeleteCommentAsync_DeletesAndSaves_WhenIsAdminTrue()
        {
            Comment comment = new Comment { Id = 1, UserId = "other-user", Content = "Test" };

            commentRepositoryMock
                .Setup(r => r.GetByIdAsync(1))
                .ReturnsAsync(comment);

            commentRepositoryMock
                .Setup(r => r.DeleteAsync(1))
                .Returns(Task.CompletedTask);

            commentRepositoryMock
                .Setup(r => r.SaveChangesAsync())
                .Returns(Task.CompletedTask);

            await commentService.DeleteCommentAsync(1, "different-user", isAdmin: true);

            commentRepositoryMock.Verify(r => r.DeleteAsync(1), Times.Once);

            commentRepositoryMock.Verify(r => r.SaveChangesAsync(), Times.Once);
        }

        [Test]
        public async Task AdminDeleteCommentAsync_DeletesAndSaves_Successfully()
        {
            Comment comment = new Comment { Id = 1, UserId = "user-123", Content = "Test" };

            commentRepositoryMock
                .Setup(r => r.GetByIdAsync(1))
                .ReturnsAsync(comment);

            commentRepositoryMock
                .Setup(r => r.DeleteAsync(1))
                .Returns(Task.CompletedTask);

            commentRepositoryMock
                .Setup(r => r.SaveChangesAsync())
                .Returns(Task.CompletedTask);

            await commentService.AdminDeleteCommentAsync(1);

            commentRepositoryMock.Verify(r => r.DeleteAsync(1), Times.Once);

            commentRepositoryMock.Verify(r => r.SaveChangesAsync(), Times.Once);
        }

        [Test]
        public async Task AdminDeleteCommentAsync_ThrowsArgumentException_WhenCommentNotFound()
        {
            commentRepositoryMock
                .Setup(r => r.GetByIdAsync(999))
                .ReturnsAsync((Comment?)null);

            Assert.ThrowsAsync<ArgumentException>(
                async () => await commentService.AdminDeleteCommentAsync(999));
        }

        [Test]
        public async Task GetCommentsByCollectionIdAsync_ReturnsEmpty_WhenNoComments()
        {
            List<Comment> comments = new List<Comment>();

            commentRepositoryMock
                .Setup(r => r.All())
                .Returns(comments.AsQueryable().BuildMock());

            IEnumerable<CommentViewModel> result = await commentService
                .GetCommentsByCollectionIdAsync(1);

            Assert.That(result, Is.Empty);
        }

        [Test]
        public async Task GetCommentsByCollectionIdAsync_ReturnsComments_ForCorrectCollection()
        {
            ApplicationUser commenter = new ApplicationUser
            {
                Id = "user-123",
                UserName = "commenter@test.com",
                AvatarUrl = "/images/avatar.jpg"
            };

            dbContext.Users.Add(commenter);

            await dbContext.SaveChangesAsync();

            List<Comment> comments = new List<Comment>
            {
                new Comment
                {
                    Id = 1,
                    Content = "Amazing games!",
                    CollectionId = 5,
                    UserId = "user-123",
                    CreatedOn = new DateTime(2024, 6, 1),
                    User = new ApplicationUser { UserName = "commenter@test.com" }
                },
                new Comment
                {
                    Id = 2,
                    Content = "Wrong collection",
                    CollectionId = 99,
                    UserId = "user-123",
                    CreatedOn = new DateTime(2024, 6, 2),
                    User = new ApplicationUser { UserName = "commenter@test.com" }
                }
            };

            commentRepositoryMock
                .Setup(r => r.All())
                .Returns(comments.AsQueryable().BuildMock());

            IEnumerable<CommentViewModel> result = await commentService
                .GetCommentsByCollectionIdAsync(5);

            List<CommentViewModel> resultList = result.ToList();

            Assert.That(resultList.Count, Is.EqualTo(1));

            Assert.That(resultList[0].Content, Is.EqualTo("Amazing games!"));

            Assert.That(resultList[0].UserId, Is.EqualTo("user-123"));
        }

        [Test]
        public async Task GetCommentsByCollectionIdAsync_ReturnsComments_OrderedByCreatedOnDescending()
        {
            ApplicationUser commenter = new ApplicationUser
            {
                Id = "user-123",
                UserName = "commenter@test.com",
                AvatarUrl = null
            };

            dbContext.Users.Add(commenter);

            await dbContext.SaveChangesAsync();

            List<Comment> comments = new List<Comment>
            {
                new Comment
                {
                    Id = 1,
                    Content = "First comment",
                    CollectionId = 1,
                    UserId = "user-123",
                    CreatedOn = new DateTime(2024, 1, 1),
                    User = new ApplicationUser { UserName = "commenter@test.com" }
                },
                new Comment
                {
                    Id = 2,
                    Content = "Latest comment",
                    CollectionId = 1,
                    UserId = "user-123",
                    CreatedOn = new DateTime(2024, 6, 1),
                    User = new ApplicationUser { UserName = "commenter@test.com" }
                }
            };

            commentRepositoryMock
                .Setup(r => r.All())
                .Returns(comments.AsQueryable().BuildMock());

            IEnumerable<CommentViewModel> result = await commentService
                .GetCommentsByCollectionIdAsync(1);

            List<CommentViewModel> resultList = result.ToList();

            Assert.That(resultList.Count, Is.EqualTo(2));

            Assert.That(resultList[0].Content, Is.EqualTo("Latest comment"));

            Assert.That(resultList[1].Content, Is.EqualTo("First comment"));
        }
    }
}