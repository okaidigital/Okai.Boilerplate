using Microsoft.EntityFrameworkCore;
using Okai.Boilerplate.Domain.Entities.Abstract;
using Okai.Boilerplate.Infrastructure.Data.Seed.Configuration;

namespace Okai.Boilerplate.Infrastructure.Data.RelationalDatabase
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<Event>();
            modelBuilder.ToSeed();
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }
    }
}