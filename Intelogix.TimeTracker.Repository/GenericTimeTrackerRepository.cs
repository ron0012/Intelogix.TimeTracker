using Microsoft.EntityFrameworkCore;
using Intelogix.TimeTracker.Data;
using System.Linq.Expressions;

namespace Intelogix.TimeTracker.Repository
{
    public class GenericTimeTrackerRepository<TEntity> : IGenericTimeTrackerRepository<TEntity> where TEntity : class
    {
        private readonly DbSet<TEntity> _dbSet;
        private readonly TimeTrackerDbContext _context;
        public GenericTimeTrackerRepository(TimeTrackerDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }
        public async Task AddAllAsync(IEnumerable<TEntity> entities)
        {
            await _dbSet.AddRangeAsync(entities);
        }

        public async Task AddAsync(TEntity entity)
        {
            await _context.AddAsync(entity);
        }

        public void Delete(TEntity entity)
        {
            _dbSet.Remove(entity);
        }

        public void DeleteAll(IEnumerable<TEntity> entities)
        {
            _dbSet.RemoveRange(entities);
        }

        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbSet.AsNoTracking().Where(predicate).SingleOrDefaultAsync();
        }

        public async Task<TEntity> GetIncludeAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes)
        {
            return await includes.Aggregate(_dbSet.AsNoTracking().Where(predicate), (current, includeProperty) => current.Include(includeProperty)).SingleOrDefaultAsync();
        }
        public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> where)
        {
            var query = _dbSet.AsNoTracking().Where(where);
            return await query.ToListAsync();
        }
        public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> where, int take, int skip)
        {
            var query = _dbSet.AsNoTracking().Where(where);
            return await query.Skip(skip).Take(take).ToListAsync();
        }
        public void Update(TEntity entity)
        {
            _dbSet.Update(entity);
        }

        public void UpdateAll(IEnumerable<TEntity> entities)
        {
            _dbSet.UpdateRange(entities);
        }
        public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> where, int take, int skip, Expression<Func<TEntity, object>> orderBy, params Expression<Func<TEntity, object>>[] includes)
        {
            var query = includes.Aggregate(_dbSet.AsNoTracking().Where(where), (current, includeProperty) => current.Include(includeProperty));
            return await query.OrderByDescending(orderBy).Skip(skip).Take(take).ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> where, params Expression<Func<TEntity, object>>[] includes)
        {
            var query = includes.Aggregate(_dbSet.AsNoTracking().Where(where), (current, includeProperty) => current.Include(includeProperty));
            return await query.ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(params Expression<Func<TEntity, object>>[] includes)
        {
            var query = includes.Aggregate(_dbSet.AsNoTracking(), (current, includeProperty) => current.Include(includeProperty));
            return await query.ToListAsync();
        }

        public async Task<int> CountAsync()
        {
            return await _dbSet.AsNoTracking().CountAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _dbSet.AsNoTracking().ToListAsync();
        }
    }
}
