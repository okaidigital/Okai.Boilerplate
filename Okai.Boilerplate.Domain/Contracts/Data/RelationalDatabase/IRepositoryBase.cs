using Okai.Boilerplate.Domain.Entities.Abstract;
using System.Linq.Expressions;

namespace Okai.Boilerplate.Domain.Contracts.Data.RelationalDatabase
{
    public interface IRepositoryBase<T> where T : Entity
    {
        Task<int> Count(Expression<Func<T, bool>> expression);
        Task Create(T entity);
        Task Delete(T entity);
        Task<IQueryable<T>> Extract(Expression<Func<T, T>> extraction, IQueryable<T> entities);
        Task<IQueryable<T>> FindAll(bool trackChanges);
        Task<IQueryable<T>> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges);
        Task<T?> FindById(int id);
        T? Find(int id);
        Task<T?> FirstOrDefault(Expression<Func<T, bool>> expression, bool trackChanges);
        Task<T?> FirstOrDefaultWithInclude<TInclude>(Expression<Func<T, bool>> expression, Expression<Func<T, TInclude>> includeExpression, bool trackChanges);

        Task<IListResponse<T>> ListByCondition(Expression<Func<T, bool>> expression, bool trackChanges, int skip, int take);

        Task LoadRelated<TProperty>(T entity, Expression<Func<T, TProperty?>> property) where TProperty : class;
        Task LoadRelated(T entity, params Expression<Func<T, object>>[] properties);
        Task LoadRelatedCollection<TProperty>(T entity, Expression<Func<T, IEnumerable<TProperty>>> property) where TProperty : class;
        Task LoadRelatedCollection<TProperty>(IEnumerable<T> collection, Expression<Func<T, IEnumerable<TProperty>>> property) where TProperty : class;
        Task LoadRelatedCollection<TProperty>(T entity, Expression<Func<T, IEnumerable<TProperty>>> property, int skip,
            int take, Expression<Func<TProperty, bool>> expression) where TProperty : class;

        Task<T?> Single(IQueryable<T> entities);
        Task Update(T entity);
    }
}
