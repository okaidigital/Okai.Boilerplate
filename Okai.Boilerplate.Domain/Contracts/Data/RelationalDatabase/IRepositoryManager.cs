using Okai.Boilerplate.Domain.Contracts.Data.RelationalDatabase.Repositories;

namespace Okai.Boilerplate.Domain.Contracts.Data.RelationalDatabase
{
    public interface IRepositoryManager
    {
        IExampleRepository ExampleRepository { get; }

        Task Save();
    }
}
