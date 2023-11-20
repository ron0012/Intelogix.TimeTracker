using System.Linq.Expressions;

namespace Intelogix.TimeTracker.Repository
{
    public interface IGenericTimeTrackerRepository<TEntity> where TEntity : class
    {
        Task AddAsync(TEntity entity);
        Task AddAllAsync(IEnumerable<TEntity> entities);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        void DeleteAll(IEnumerable<TEntity> entities);
        void UpdateAll(IEnumerable<TEntity> entities);
        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> where);
        Task<TEntity> GetIncludeAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes);
        Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> where, int take, int skip,
            Expression<Func<TEntity, object>> orderBy, params Expression<Func<TEntity, object>>[] includes);
        Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> where, int take, int skip);
        Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> where);
        Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> where, params Expression<Func<TEntity, object>>[] includes);
        Task<IEnumerable<TEntity>> GetAllAsync(params Expression<Func<TEntity, object>>[] includes);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<int> CountAsync();
    }
}