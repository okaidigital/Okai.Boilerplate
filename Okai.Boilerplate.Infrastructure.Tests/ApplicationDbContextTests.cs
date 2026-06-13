using Microsoft.EntityFrameworkCore;
using Okai.Boilerplate.Infrastructure.Data.RelationalDatabase;

namespace Okai.Boilerplate.Infrastructure.Tests;

[TestClass]
public sealed class ApplicationDbContextTests
{
    [TestMethod]
    public void Constructor_CreatesModelWithInMemoryProvider()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        using var context = new ApplicationDbContext(options);

        Assert.IsNotNull(context.Model);
    }
}
