using MockQueryable.Moq;
using Moq;
using TheBigThree.Data.Models;
using TheBigThree.Services.Core.Repositories;
using TheBigThree.Web.ViewModels;

namespace TheBigThree.Tests.Services
{
    [TestFixture]
    public class CommentServiceTests
    {
        private Mock<IRepository<Comment>> commentRepositoryMock;
        private TheBigThree.Services.CommentService commentService;

        [SetUp]
        public void SetUp()
        {
            commentRepositoryMock = new Mock<IRepository<Comment>>();

            commentService = new TheBigThree.Services.CommentService(
                commentRepositoryMock.Object,
                null!);
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
    }
}