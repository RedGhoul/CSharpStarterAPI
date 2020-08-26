using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Common.Tests.Integration
{
    public static class ApplicationDBContextInMemoryFactory
    {
        public static ApplicationDbContext Generate()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
              .UseInMemoryDatabase(databaseName: "Main" + System.Guid.NewGuid().ToString()).Options;

            ApplicationDbContext context = new ApplicationDbContext(options);
            IntegrationDataUtilities.ReinitializeDbForTests(context);

            return context;
        }
    }
}
