using Okai.Boilerplate.Domain.Contracts.Data.RelationalDatabase;
using Okai.Boilerplate.Domain.Contracts.Data.RelationalDatabase.Repositories;
using Okai.Boilerplate.Infrastructure.Data.RelationalDatabase.Repositories;

namespace Okai.Boilerplate.Infrastructure.Data.RelationalDatabase
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly ApplicationDbContext _repositoryContext;
        private IExampleRepository? _exampleRepository;

        public RepositoryManager(ApplicationDbContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
        }

        public IExampleRepository ExampleRepository => _exampleRepository ??=
            new ExampleRepository(_repositoryContext);


        public async Task Save() => await _repositoryContext.SaveChangesAsync();
    }
}