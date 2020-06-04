using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using TemplateAPI.DAL.Commands;
using TemplateAPI.DAL.Executers;
using TemplateAPI.DAL.Queries;
using TemplateAPI.DAL.Repos;
using TemplateAPI.Models.Enities;
using Xunit;

namespace TemplateAPI.Tests.Repos.Tests
{
    public class EventRepositoryTests
    {
        [Fact]
        public async Task GetByIdAsync_Returns_Entity()
        {
            //// Arrange
            //IConfiguration configuration = new ConfigurationBuilder()
            //.AddJsonFile("appsettings.json", optional: false)
            //.Build();

            //ICommandEvent _commandEvent = new CommandEvent();
            //IExecuters _executers = new Executers();
            //var controller = new EventRepository(configuration, _commandEvent, _executers);

            //// Act
            //var result = await controller.GetByIdAsync(9999);

            //// Assert
            //var Result = Assert.IsType<Event>(result);
        }
    }
}
