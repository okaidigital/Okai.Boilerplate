using Okai.Boilerplate.Domain.Contracts.Data.RelationalDatabase.Repositories;
using Okai.Boilerplate.Domain.Entities;

namespace Okai.Boilerplate.Infrastructure.Data.RelationalDatabase.Repositories
{
    public class ExampleRepository : RepositoryBase<Example>, IExampleRepository
    {
        public ExampleRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {

        }
    }
}
