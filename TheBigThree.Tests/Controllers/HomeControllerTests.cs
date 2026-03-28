using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using TheBigThree.Controllers;
using TheBigThree.Services.Core.Interfaces;
using TheBigThree.Web.ViewModels;

namespace TheBigThree.Tests.Controllers
{
    [TestFixture]
    public class HomeControllerTests
    {
        private Mock<ILogger<HomeController>> loggerMock;
        private HomeController controller;

        [SetUp]
        public void SetUp()
        {
            loggerMock = new Mock<ILogger<HomeController>>();

            controller = new HomeController(loggerMock.Object);
        }

        [TearDown]
        public void TearDown()
        {
            controller.Dispose();
        }

        [Test]
        public void Index_ReturnsViewResult()
        {
            IActionResult result = controller.Index();

            Assert.That(result, Is.InstanceOf<ViewResult>());
        }

        [Test]
        public void Privacy_ReturnsViewResult()
        {
            IActionResult result = controller.Privacy();

            Assert.That(result, Is.InstanceOf<ViewResult>());
        }

        [Test]
        public void Error_Returns404View_WhenStatusCodeIs404()
        {
            IActionResult result = controller.Error(404);

            ViewResult viewResult = (ViewResult)result;

            Assert.That(viewResult.ViewName, Is.EqualTo("Error404"));
        }

        [Test]
        public void Error_Returns500View_WhenStatusCodeIsNot404()
        {
            IActionResult result = controller.Error(500);

            ViewResult viewResult = (ViewResult)result;

            Assert.That(viewResult.ViewName, Is.EqualTo("Error500"));
        }

        [Test]
        public void Error_Returns500View_WhenStatusCodeIsNull()
        {
            IActionResult result = controller.Error(null);

            ViewResult viewResult = (ViewResult)result;

            Assert.That(viewResult.ViewName, Is.EqualTo("Error500"));
        }
    }
}