using Okai.Boilerplate.Domain.Entities.Abstract;

namespace Okai.Boilerplate.Domain.Contracts.Data.RelationalDatabase
{
    public interface IListResponse<TEntity> where TEntity : Entity
    {
        public int TotalItems { get; set; }
        public IEnumerable<TEntity> Data { get; set; }

        public void Deconstruct(out int totalItems, out IEnumerable<TEntity> data)
        {
            totalItems = TotalItems;
            data = Data;
        }
    }

    public class ListResponse<TEntity> : IListResponse<TEntity> where TEntity : Entity
    {
        public int TotalItems { get; set; }
        public IEnumerable<TEntity> Data { get; set; }

        public ListResponse()
        {

        }

        public ListResponse(int totalItems, IEnumerable<TEntity> data)
        {
            TotalItems = totalItems;
            Data = data;
        }
    }
}
