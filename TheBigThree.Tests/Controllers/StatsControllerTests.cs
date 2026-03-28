using Microsoft.AspNetCore.Mvc;
using Moq;
using TheBigThree.Controllers;
using TheBigThree.Services.Core.Interfaces;
using TheBigThree.Web.ViewModels;

namespace TheBigThree.Tests.Controllers
{
    [TestFixture]
    public class StatsControllerTests
    {
        private Mock<IStatsService> statsServiceMock;
        private StatsController controller;

        [SetUp]
        public void SetUp()
        {
            statsServiceMock = new Mock<IStatsService>();
            controller = new StatsController(statsServiceMock.Object);
        }

        [TearDown]
        public void TearDown()
        {
            controller.Dispose();
        }

        [Test]
        public async Task Index_ReturnsViewResult()
        {
            statsServiceMock
                .Setup(s => s.GetStatsAsync())
                .ReturnsAsync(new StatsViewModel());

            IActionResult result = await controller.Index();

            Assert.That(result, Is.InstanceOf<ViewResult>());
        }

        [Test]
        public async Task Index_PassesViewModelToView()
        {
            StatsViewModel expectedModel = new StatsViewModel
            {
                TotalCollections = 10,
                TotalComments = 25,
                TotalStars = 50
            };

            statsServiceMock
                .Setup(s => s.GetStatsAsync())
                .ReturnsAsync(expectedModel);

            IActionResult result = await controller.Index();

            ViewResult viewResult = (ViewResult)result;

            Assert.That(viewResult.Model, Is.EqualTo(expectedModel));
        }

        [Test]
        public async Task Index_CallsGetStatsAsync_ExactlyOnce()
        {
            statsServiceMock
                .Setup(s => s.GetStatsAsync())
                .ReturnsAsync(new StatsViewModel());

            await controller.Index();

            statsServiceMock.Verify(s => s.GetStatsAsync(), Times.Once);
        }
    }
}
