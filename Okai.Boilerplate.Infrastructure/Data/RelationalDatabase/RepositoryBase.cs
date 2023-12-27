using Microsoft.EntityFrameworkCore;
using Okai.Boilerplate.Domain.Contracts.Data.RelationalDatabase;
using Okai.Boilerplate.Domain.Entities.Abstract;
using System.Collections;
using System.Linq.Expressions;
using System.Reflection;

namespace Okai.Boilerplate.Infrastructure.Data.RelationalDatabase
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : Entity
    {
        protected ApplicationDbContext ApplicationDbContext;

        protected RepositoryBase(ApplicationDbContext applicationDbContext)
        {
            ApplicationDbContext = applicationDbContext;
        }

        public async Task<int> Count(Expression<Func<T, bool>> expression) =>
            await Task.Run(() => ApplicationDbContext.Set<T>().AsNoTracking().Where(expression).CountAsync());

        public async Task Create(T entity) =>
            await ApplicationDbContext.Set<T>().AddAsync(entity);

        public async Task Delete(T entity)
        {
            ApplicationDbContext.Set<T>().Remove(entity);
            await Task.CompletedTask;
        }

        public async Task<IQueryable<T>> Extract(Expression<Func<T, T>> extraction, IQueryable<T> entities) =>
            await Task.Run(() => entities.Select(extraction));

        public T? Find(int id) => ApplicationDbContext.Set<T>().Find(id);

        public async Task<IQueryable<T>> FindAll(bool trackChanges) =>
            await Task.Run(() => trackChanges ? ApplicationDbContext.Set<T>() :
                ApplicationDbContext.Set<T>().AsNoTracking());

        public async Task<IQueryable<T>> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges) =>
            await Task.Run(() => trackChanges ? ApplicationDbContext.Set<T>().Where(expression) :
                ApplicationDbContext.Set<T>().Where(expression).AsNoTracking());

        public async Task<T?> FindById(int id) =>
            await ApplicationDbContext.Set<T>().FindAsync(id);

        public async Task<T?> FirstOrDefault(Expression<Func<T, bool>> expression, bool trackChanges) =>
            await Task.Run(() => trackChanges ? ApplicationDbContext.Set<T>().FirstOrDefaultAsync(expression) :
                ApplicationDbContext.Set<T>().AsNoTracking().FirstOrDefaultAsync(expression));

        public async Task<T?> FirstOrDefaultWithInclude<TInclude>(Expression<Func<T, bool>> expression, Expression<Func<T, TInclude>> includeExpression, bool trackChanges) =>
            await Task.Run(() => trackChanges ? ApplicationDbContext.Set<T>().Include(includeExpression).FirstOrDefaultAsync(expression) :
                ApplicationDbContext.Set<T>().AsNoTracking().Include(includeExpression).FirstOrDefaultAsync(expression));

        public async Task<IListResponse<T>> ListByCondition(Expression<Func<T, bool>> expression, bool trackChanges,
            int skip, int take)
        {
            return await Task.Run(() => trackChanges
                ? ApplicationDbContext.Set<T>().Where(expression).GroupBy(e => true)
                    .Select(g => new ListResponse<T>
                    {
                        TotalItems = g.Count(),
                        Data = g.Skip(skip).Take(take)
                    }).FirstAsync()

                : ApplicationDbContext.Set<T>().Where(expression).AsNoTracking().GroupBy(e => true)
                    .Select(g => new ListResponse<T>
                    {
                        TotalItems = g.Count(),
                        Data = g.Skip(skip).Take(take)
                    }).FirstAsync());
        }

        public async Task LoadRelated<TProperty>(T entity, Expression<Func<T, TProperty?>> property) where TProperty : class
        {
            await Task.Run(() => ApplicationDbContext.Entry(entity).Reference(property).LoadAsync());
        }

        public async Task LoadRelatedCollection<TProperty>(T entity, Expression<Func<T, IEnumerable<TProperty>>> property) where TProperty : class
        {
            await Task.Run(() => ApplicationDbContext.Entry(entity).Collection(property).LoadAsync());
        }

        public async Task LoadRelatedCollection<TProperty>(IEnumerable<T> collection, Expression<Func<T, IEnumerable<TProperty>>> property) where TProperty : class
        {
            await Task.Run(async () =>
            {
                foreach (var entity in collection)
                    await ApplicationDbContext.Entry(entity).Collection(property)
                        .Query()
                        .AsSplitQuery()
                        .LoadAsync();
            });
        }

        public async Task LoadRelated(T entity, params Expression<Func<T, object>>[] properties)
        {
            await Task.Run(async () =>
            {
                var entry = ApplicationDbContext.Entry(entity);

                foreach (var property in properties)
                {
                    if (property.Body is not MemberExpression { Member: PropertyInfo propertyInfo }) continue;

                    if (typeof(IEnumerable).IsAssignableFrom(propertyInfo.PropertyType))
                        await entry.Collection(propertyInfo.Name).LoadAsync();
                    else
                        await entry.Reference(propertyInfo.Name).LoadAsync();
                }
            });
        }

        public async Task LoadRelatedCollection<TProperty>(T entity, Expression<Func<T, IEnumerable<TProperty>>> property, 
            int skip, int take, Expression<Func<TProperty, bool>> expression) where TProperty : class
        {
            await Task.Run(() => ApplicationDbContext.Entry(entity).Collection(property).Query().Where(expression).Skip(skip).Take(take).LoadAsync());
        }

        public async Task<T?> Single(IQueryable<T> entities) =>
            await entities.FirstOrDefaultAsync();

        public async Task Update(T entity)
        {
            ApplicationDbContext.Set<T>().Update(entity);
            await Task.CompletedTask;
        }
    }
}
