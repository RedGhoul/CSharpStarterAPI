using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Threading.Tasks;
using TemplateAPI.Controllers.V1;
using TemplateAPI.DAL.Repos;
using TemplateAPI.Tests.Helpers;
using Xunit;

namespace TemplateAPI.Tests.Controller.Tests.V1
{
    public class EventsControllerTests
    {
        [Fact]
        public async Task GetById_Returns_NotFound()
        {
            // Arrange
            var mockPointRepo = new Mock<IEventRepository>();
            mockPointRepo.Setup(repo => repo.GetByIdAsync(9999))
                .ReturnsAsync(EventsRepoHelpers.GetByIdAsyncNull(9999));
            // var mockLogger = new Mock<ILogger<HomeController>>();
            var mockMapper = new Mock<IMapper>();
            var controller = new EventsController(mockPointRepo.Object, mockMapper.Object);

            // Act
            var result = await controller.GetById(9999);

            // Assert
            var Result = Assert.IsAssignableFrom<NotFoundResult>(result);
        }

        [Fact]
        public async Task GetById_Returns_Ok()
        {
            // Arrange
            var mockPointRepo = new Mock<IEventRepository>();
            mockPointRepo.Setup(repo => repo.GetByIdAsync(9999))
                .ReturnsAsync(EventsRepoHelpers.GetByIdAsync(9999));
            var mockMapper = new Mock<IMapper>();
            var controller = new EventsController(mockPointRepo.Object, mockMapper.Object);

            // Act
            var result = await controller.GetById(9999);

            // Assert
            var Result = Assert.IsAssignableFrom<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetByGroupId_Returns_NotFound()
        {
            // Arrange
            var mockPointRepo = new Mock<IEventRepository>();
            mockPointRepo.Setup(repo => repo.GetByIdAsync(9999))
                .ReturnsAsync(EventsRepoHelpers.GetByIdAsyncNull(9999));
            var mockMapper = new Mock<IMapper>();
            var controller = new EventsController(mockPointRepo.Object, mockMapper.Object);

            // Act
            var result = await controller.GetById(9999);

            // Assert
            var Result = Assert.IsAssignableFrom<NotFoundResult>(result);
        }

        [Fact]
        public async Task GetByGroupId_Returns_Ok()
        {
            // Arrange
            var mockPointRepo = new Mock<IEventRepository>();
            mockPointRepo.Setup(repo => repo.GetByIdAsync(9999))
                .ReturnsAsync(EventsRepoHelpers.GetByIdAsync(9999));
            var mockMapper = new Mock<IMapper>();
            var controller = new EventsController(mockPointRepo.Object, mockMapper.Object);

            // Act
            var result = await controller.GetById(9999);

            // Assert
            var Result = Assert.IsAssignableFrom<OkObjectResult>(result);
        }
    }
}
