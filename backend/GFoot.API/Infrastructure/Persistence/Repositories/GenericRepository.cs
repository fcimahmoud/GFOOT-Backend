
using System.Linq.Expressions;

namespace Persistence.Repositories
{
    public class GenericRepository<TEntity, TKey> (GFootDbContext context)
        : IGenericRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        public async Task AddAsync(TEntity entity)
            => await context.Set<TEntity>().AddAsync(entity);

        public void Delete(TEntity entity)
            => context.Set<TEntity>().Remove(entity);

        public async Task<IEnumerable<TEntity>> GetAllAsync(bool trackChanges = true)
            => trackChanges ? await context.Set<TEntity>().ToListAsync()
                            : await context.Set<TEntity>().AsNoTracking().ToListAsync();

        public async Task<TEntity?> GetAsync(TKey id)
            => await context.Set<TEntity>().FindAsync(id);

        public void Update(TEntity entity)
            => context.Set<TEntity>().Update(entity);

        // Retrieve a single entity by condition
        public async Task<TEntity?> GetByConditionAsync(Expression<Func<TEntity, bool>> condition)
            => await context.Set<TEntity>().FirstOrDefaultAsync(condition);

        // Retrieve all matching entities with optional tracking
        public async Task<IEnumerable<TEntity>> GetAllByConditionAsync(Expression<Func<TEntity, bool>> condition)
            => await context.Set<TEntity>().Where(condition).ToListAsync();

        // Retrieve entity with Includes (for related data)
        public async Task<TEntity?> GetWithIncludesAsync(Expression<Func<TEntity, bool>> condition, params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = context.Set<TEntity>();

            foreach (var include in includes)
                query = query.Include(include);

            return await query.FirstOrDefaultAsync(condition);
        }

        // Retrieve all matching entities with Includes
        public async Task<IEnumerable<TEntity>> GetAllWithIncludesAsync(Expression<Func<TEntity, bool>> condition, params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = context.Set<TEntity>();

            foreach (var include in includes)
                query = query.Include(include);

            return await query.Where(condition).ToListAsync();
        }

        // Retrieve a sorted list based on a key selector
        public async Task<IEnumerable<TEntity>> GetSortedAsync<TKeySelector>(Expression<Func<TEntity, TKeySelector>> orderBy, bool ascending = true)
        {
            IQueryable<TEntity> query = context.Set<TEntity>();

            query = ascending ? query.OrderBy(orderBy) : query.OrderByDescending(orderBy);

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAllByConditionSortedAsync<TKeySelector>( Expression<Func<TEntity, bool>> condition,
            Expression<Func<TEntity, TKeySelector>> orderBy,
            bool ascending = true)
        {
            IQueryable<TEntity> query = context.Set<TEntity>();

            query = ascending ? query.Where(condition).OrderBy(orderBy) : query.Where(condition).OrderByDescending(orderBy);

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<TResult>> GetAllByConditionGroupedSortedAsync<TKeySelector, TGroupKey, TResult>
            ( Expression<Func<TEntity, bool>> condition,
              Expression<Func<TEntity, TGroupKey>> groupBy,
              Expression<Func<IGrouping<TGroupKey, TEntity>, TKeySelector>> orderBy,
              Expression<Func<IGrouping<TGroupKey, TEntity>, TResult>> selector,
              bool ascending = true)
        {
            IQueryable<TEntity> query = context.Set<TEntity>().Where(condition);

            var groupedQuery = query.GroupBy(groupBy);

            var sortedQuery = ascending
                ? groupedQuery.OrderBy(orderBy)
                : groupedQuery.OrderByDescending(orderBy);

            return await sortedQuery.Select(selector).ToListAsync();
        }
    }
}
