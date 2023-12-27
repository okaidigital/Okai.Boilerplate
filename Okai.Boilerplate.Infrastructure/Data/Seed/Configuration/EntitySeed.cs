using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Okai.Boilerplate.Infrastructure.Data.Seed.Configuration
{
    public abstract class EntitySeed : IEntitySeed
    {
        internal readonly DateTime CriadoEm = new(2023, 1, 1);
        public abstract void Seed(ModelBuilder modelBuilder);

        protected EntitySeed()
        {
        }
    }

    public interface IEntitySeed
    {
        void Seed(ModelBuilder modelBuilder);
    }

    internal static class SeedBuilder
    {
        public static void ToSeed(this ModelBuilder modelBuilder)
        {
            var types =
                Assembly.GetExecutingAssembly().GetTypes()
                    .Where(t => t is { IsInterface: false, IsAbstract: false } &&
                    t.GetInterfaces().Contains(typeof(IEntitySeed)));

            foreach (var type in types)
            {

                var instance = (IEntitySeed?)Activator.CreateInstance(type);
                instance?.Seed(modelBuilder);
            }
        }
    }
}
