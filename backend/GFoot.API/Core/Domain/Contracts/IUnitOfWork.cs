
namespace Domain.Contracts
{
    public interface IUnitOfWork
    {
        public Task<int> SaveChangesAsynk();
        public IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>() 
            where TEntity : BaseEntity<TKey>;
    }
}
