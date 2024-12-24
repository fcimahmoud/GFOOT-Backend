
namespace Persistence.Repositories
{
    public class UnitOfWork 
        : IUnitOfWork
    {
        private readonly GFootDbContext _context;
        private readonly ConcurrentDictionary<string, object> _repositories;
        public UnitOfWork(GFootDbContext context)
        {
            _context = context;
            _repositories = new();
        }

        public IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>() 
            where TEntity : BaseEntity<TKey>
          => (IGenericRepository<TEntity, TKey>) // Casting return type to (IGenericRepository)
            _repositories.GetOrAdd(typeof(TEntity).Name, _ => new GenericRepository<TEntity, TKey>(_context));

        public async Task<int> SaveChangesAsynk()
            => await _context.SaveChangesAsync();
    }
}
