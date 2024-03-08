namespace EducationPortal.Infrastructure.Repositories
{
    public class Repository<T> : IGenericRepository<T> where T : DataBaseEntity
    {
        protected readonly DbContext Context;
        public Repository(DbContext context)
        {
            Context = context;
        }
        public async Task<T> GetAsync(int id)
        {
            return await Context.Set<T>()
                .FirstOrDefaultAsync(u => u.Id == id && u.IsDeleted == false);
        }
        public async Task<ICollection<T>> GetAllAsync()
        {
            return await Context.Set<T>()
                .Where(item => item.IsDeleted == false)
                .ToListAsync();
        }
        public async Task<T> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return await Context.Set<T>()
                .Where(predicate)
                .FirstOrDefaultAsync(c => c.IsDeleted == false);
        }
        public async Task<ICollection<T>> FindManyAsync(Expression<Func<T, bool>> predicate)
        {
            return await Context.Set<T>()
                .Where(predicate)
                .Where(c => c.IsDeleted == false)
                .ToListAsync();
        }
        public async Task AddAsync(T item)
        {
            await Context.Set<T>().AddAsync(item);
        }
        public async Task AddManyAsync(ICollection<T> items)
        {
            await Context.Set<T>().AddRangeAsync(items);
        }
        public void Remove(T item)
        {
            if (item != null)
                item.IsDeleted = true;
        }
        public Task RemoveManyAsync(ICollection<T> items)
        {
            throw new NotImplementedException();
        }
        public async Task<bool> IsExisting(int id)
        {
            return await Context.Set<T>().AnyAsync(c => c.Id == id && c.IsDeleted == false);
        }
    }
}
