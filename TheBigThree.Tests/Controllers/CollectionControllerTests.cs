using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;
using System.Security.Claims;
using TheBigThree.Controllers;
using TheBigThree.Services.Core.Interfaces;
using TheBigThree.Web.ViewModels;

namespace TheBigThree.Tests.Controllers
{
    [TestFixture]
    public class CollectionControllerTests
    {
        private Mock<ICollectionService> collectionServiceMock;
        private Mock<ICommentService> commentServiceMock;
        private Mock<ILikeService> likeServiceMock;
        private CollectionController controller;

        private const string TestUserId = "user-123";

        [SetUp]
        public void SetUp()
        {
            collectionServiceMock = new Mock<ICollectionService>();
            commentServiceMock = new Mock<ICommentService>();
            likeServiceMock = new Mock<ILikeService>();

            controller = new CollectionController(
                collectionServiceMock.Object,
                commentServiceMock.Object,
                likeServiceMock.Object);

            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, TestUserId),
                new Claim(ClaimTypes.Name, "testuser@test.com")
            };

            ClaimsIdentity identity = new ClaimsIdentity(claims, "TestAuth");

            ClaimsPrincipal principal = new ClaimsPrincipal(identity);

            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = principal
                }
            };

            controller.TempData = new TempDataDictionary(
                controller.ControllerContext.HttpContext,
                Mock.Of<ITempDataProvider>());
        }

        [TearDown]
        public void TearDown()
        {
            controller.Dispose();
        }

        [Test]
        public async Task All_ReturnsViewResult_WithQueryModel()
        {
            CollectionQueryModel query = new CollectionQueryModel();

            CollectionQueryModel returnedQuery = new CollectionQueryModel { TotalCollections = 5 };

            collectionServiceMock
                .Setup(s => s.GetAllCollectionsAsync(query))
                .ReturnsAsync(returnedQuery);

            collectionServiceMock
                .Setup(s => s.UserHasCollectionAsync(TestUserId))
                .ReturnsAsync(true);

            IActionResult result = await controller.All(query);

            ViewResult viewResult = (ViewResult)result;

            Assert.That(viewResult.Model, Is.EqualTo(returnedQuery));
        }

        [Test]
        public async Task Mine_ReturnsViewResult_WithCollection()
        {
            CollectionMineViewModel mineViewModel = new CollectionMineViewModel
            {
                Id = 1,
                Title = "My Big Three"
            };

            collectionServiceMock
                .Setup(s => s.GetMineCollectionAsync(TestUserId))
                .ReturnsAsync(mineViewModel);

            IActionResult result = await controller.Mine();

            ViewResult viewResult = (ViewResult)result;

            Assert.That(viewResult.Model, Is.EqualTo(mineViewModel));
        }

        [Test]
        public async Task Mine_ReturnsViewWithNull_WhenUserHasNoCollection()
        {
            collectionServiceMock
                .Setup(s => s.GetMineCollectionAsync(TestUserId))
                .ReturnsAsync((CollectionMineViewModel?)null);

            IActionResult result = await controller.Mine();

            ViewResult viewResult = (ViewResult)result;

            Assert.That(viewResult.Model, Is.Null);
        }

        [Test]
        public async Task Details_RedirectsToAll_WhenCollectionNotFound()
        {
            collectionServiceMock
                .Setup(s => s.GetCollectionDetailsByIdAsync(999))
                .ReturnsAsync((CollectionDetailsViewModel?)null);

            IActionResult result = await controller.Details(999);

            RedirectToActionResult redirect = (RedirectToActionResult)result;

            Assert.That(redirect.ActionName, Is.EqualTo("All"));
        }

        [Test]
        public async Task Details_ReturnsViewResult_WhenCollectionFound()
        {
            CollectionDetailsViewModel details = new CollectionDetailsViewModel
            {
                Id = 1,
                Title = "My Big Three",
                Comments = new List<CommentViewModel>()
            };

            collectionServiceMock
                .Setup(s => s.GetCollectionDetailsByIdAsync(1))
                .ReturnsAsync(details);

            commentServiceMock
                .Setup(s => s.GetCommentsByCollectionIdAsync(1))
                .ReturnsAsync(new List<CommentViewModel>());

            likeServiceMock
                .Setup(s => s.IsStarredByUserAsync(1, TestUserId))
                .ReturnsAsync(false);

            IActionResult result = await controller.Details(1);

            Assert.That(result, Is.InstanceOf<ViewResult>());
        }

        [Test]
        public async Task Add_Get_RedirectsToMine_WhenUserAlreadyHasCollection()
        {
            collectionServiceMock
                .Setup(s => s.UserHasCollectionAsync(TestUserId))
                .ReturnsAsync(true);

            IActionResult result = await controller.Add();

            RedirectToActionResult redirect = (RedirectToActionResult)result;

            Assert.That(redirect.ActionName, Is.EqualTo("Mine"));
        }

        [Test]
        public async Task Add_Get_ReturnsViewWithForm_WhenUserHasNoCollection()
        {
            collectionServiceMock
                .Setup(s => s.UserHasCollectionAsync(TestUserId))
                .ReturnsAsync(false);

            collectionServiceMock
                .Setup(s => s.GetNewAddFormModelAsync())
                .ReturnsAsync(new CollectionFormViewModel());

            IActionResult result = await controller.Add();

            Assert.That(result, Is.InstanceOf<ViewResult>());
        }

        [Test]
        public async Task Add_Post_RedirectsToMine_OnSuccess()
        {
            collectionServiceMock
                .Setup(s => s.UserHasCollectionAsync(TestUserId))
                .ReturnsAsync(false);

            collectionServiceMock
                .Setup(s => s.AddCollectionAsync(It.IsAny<CollectionFormViewModel>(), TestUserId))
                .Returns(Task.CompletedTask);

            CollectionFormViewModel form = new CollectionFormViewModel { Title = "My Big Three" };

            IActionResult result = await controller.Add(form);

            RedirectToActionResult redirect = (RedirectToActionResult)result;

            Assert.That(redirect.ActionName, Is.EqualTo("Mine"));
        }

        [Test]
        public async Task Add_Post_RedirectsToMine_WhenUserAlreadyHasCollection()
        {
            collectionServiceMock
                .Setup(s => s.UserHasCollectionAsync(TestUserId))
                .ReturnsAsync(true);

            CollectionFormViewModel form = new CollectionFormViewModel { Title = "My Big Three" };

            IActionResult result = await controller.Add(form);

            RedirectToActionResult redirect = (RedirectToActionResult)result;

            Assert.That(redirect.ActionName, Is.EqualTo("Mine"));
        }

        [Test]
        public async Task Edit_Get_RedirectsToMine_WhenCollectionNotFound()
        {
            collectionServiceMock
                .Setup(s => s.GetCollectionForEditAsync(999, TestUserId))
                .ReturnsAsync((CollectionFormViewModel?)null);

            IActionResult result = await controller.Edit(999);

            RedirectToActionResult redirect = (RedirectToActionResult)result;

            Assert.That(redirect.ActionName, Is.EqualTo("Mine"));
        }

        [Test]
        public async Task Edit_Get_ReturnsViewWithForm_WhenCollectionFound()
        {
            CollectionFormViewModel form = new CollectionFormViewModel { Title = "My Big Three" };

            collectionServiceMock
                .Setup(s => s.GetCollectionForEditAsync(1, TestUserId))
                .ReturnsAsync(form);

            IActionResult result = await controller.Edit(1);

            ViewResult viewResult = (ViewResult)result;

            Assert.That(viewResult.Model, Is.EqualTo(form));
        }

        [Test]
        public async Task Edit_Post_RedirectsToMine_OnSuccess()
        {
            collectionServiceMock
                .Setup(s => s.EditCollectionAsync(It.IsAny<CollectionFormViewModel>(), 1, TestUserId))
                .Returns(Task.CompletedTask);

            CollectionFormViewModel form = new CollectionFormViewModel { Title = "Updated" };

            IActionResult result = await controller.Edit(1, form);

            RedirectToActionResult redirect = (RedirectToActionResult)result;

            Assert.That(redirect.ActionName, Is.EqualTo("Mine"));
        }

        [Test]
        public async Task Delete_Get_RedirectsToMine_WhenCollectionNotFound()
        {
            collectionServiceMock
                .Setup(s => s.GetCollectionForDeleteAsync(999, TestUserId))
                .ReturnsAsync((CollectionDetailsViewModel?)null);

            IActionResult result = await controller.Delete(999);

            RedirectToActionResult redirect = (RedirectToActionResult)result;

            Assert.That(redirect.ActionName, Is.EqualTo("Mine"));
        }

        [Test]
        public async Task Delete_Get_ReturnsViewWithDetails_WhenCollectionFound()
        {
            CollectionDetailsViewModel details = new CollectionDetailsViewModel { Id = 1, Title = "To Delete" };

            collectionServiceMock
                .Setup(s => s.GetCollectionForDeleteAsync(1, TestUserId))
                .ReturnsAsync(details);

            IActionResult result = await controller.Delete(1);

            ViewResult viewResult = (ViewResult)result;

            Assert.That(viewResult.Model, Is.EqualTo(details));
        }

        [Test]
        public async Task DeleteConfirmed_RedirectsToMine_OnSuccess()
        {
            collectionServiceMock
                .Setup(s => s.DeleteCollectionAsync(1, TestUserId))
                .Returns(Task.CompletedTask);

            IActionResult result = await controller.DeleteConfirmed(1);

            RedirectToActionResult redirect = (RedirectToActionResult)result;

            Assert.That(redirect.ActionName, Is.EqualTo("Mine"));
        }

        [Test]
        public async Task Star_RedirectsToDetails_OnSuccess()
        {
            likeServiceMock
                .Setup(s => s.StarCollectionAsync(1, TestUserId))
                .Returns(Task.CompletedTask);

            IActionResult result = await controller.Star(1);

            RedirectToActionResult redirect = (RedirectToActionResult)result;

            Assert.That(redirect.ActionName, Is.EqualTo("Details"));
        }

        [Test]
        public async Task Star_RedirectsToDetails_WhenInvalidOperationThrown()
        {
            likeServiceMock
                .Setup(s => s.StarCollectionAsync(1, TestUserId))
                .ThrowsAsync(new InvalidOperationException("Already starred."));

            IActionResult result = await controller.Star(1);

            RedirectToActionResult redirect = (RedirectToActionResult)result;

            Assert.That(redirect.ActionName, Is.EqualTo("Details"));

            Assert.That(controller.TempData["Error"], Is.EqualTo("Already starred."));
        }

        [Test]
        public async Task RemoveStar_RedirectsToDetails_OnSuccess()
        {
            likeServiceMock
                .Setup(s => s.RemoveStarAsync(1, TestUserId))
                .Returns(Task.CompletedTask);

            IActionResult result = await controller.RemoveStar(1);

            RedirectToActionResult redirect = (RedirectToActionResult)result;

            Assert.That(redirect.ActionName, Is.EqualTo("Details"));
        }
    }
}