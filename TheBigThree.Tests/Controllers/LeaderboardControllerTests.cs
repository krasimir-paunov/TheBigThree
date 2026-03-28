using Microsoft.AspNetCore.Mvc;
using Moq;
using TheBigThree.Controllers;
using TheBigThree.Services.Core.Interfaces;
using TheBigThree.Web.ViewModels;

namespace TheBigThree.Tests.Controllers
{
    [TestFixture]
    public class LeaderboardControllerTests
    {
        private Mock<IStatsService> statsServiceMock;
        private LeaderboardController controller;

        [SetUp]
        public void SetUp()
        {
            statsServiceMock = new Mock<IStatsService>();
            controller = new LeaderboardController(statsServiceMock.Object);
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
                .Setup(s => s.GetLeaderboardAsync())
                .ReturnsAsync(new LeaderboardViewModel());

            IActionResult result = await controller.Index();

            Assert.That(result, Is.InstanceOf<ViewResult>());
        }

        [Test]
        public async Task Index_PassesViewModelToView()
        {
            LeaderboardViewModel expectedModel = new LeaderboardViewModel
            {
                TopCollectors = new List<LeaderboardEntryViewModel>(),
                TopCommenters = new List<LeaderboardEntryViewModel>()
            };

            statsServiceMock
                .Setup(s => s.GetLeaderboardAsync())
                .ReturnsAsync(expectedModel);

            IActionResult result = await controller.Index();

            ViewResult viewResult = (ViewResult)result;

            Assert.That(viewResult.Model, Is.EqualTo(expectedModel));
        }

        [Test]
        public async Task Index_CallsGetLeaderboardAsync_ExactlyOnce()
        {
            statsServiceMock
                .Setup(s => s.GetLeaderboardAsync())
                .ReturnsAsync(new LeaderboardViewModel());

            await controller.Index();

            statsServiceMock.Verify(s => s.GetLeaderboardAsync(), Times.Once);
        }
    }
}