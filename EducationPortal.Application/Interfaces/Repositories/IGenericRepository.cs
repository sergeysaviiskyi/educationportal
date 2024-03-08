namespace EducationPortal.Application.Interfaces.Repositories
{
    public interface IGenericRepository<T> where T : DataBaseEntity
    {
        public Task<T> GetAsync(int id);
        public Task<ICollection<T>>  GetAllAsync();
        public Task<T> FindAsync(Expression<Func<T, bool>> predicate);
        public Task<ICollection<T>> FindManyAsync(Expression<Func<T, bool>> predicate);
        public Task AddAsync(T item);
        public Task AddManyAsync(ICollection<T> items);
        public void Remove(T item);
        public Task RemoveManyAsync(ICollection<T> items);
        public Task<bool> IsExisting(int id);
    }
}