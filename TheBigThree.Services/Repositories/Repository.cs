using Microsoft.EntityFrameworkCore;
using TheBigThree.Data;
using TheBigThree.Services.Core.Repositories;

namespace TheBigThree.Services.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly TheBigThreeDbContext dbContext;

        public Repository(TheBigThreeDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task AddAsync(T entity)
        {
            await dbContext.Set<T>().AddAsync(entity);
        }

        public IQueryable<T> All()
        {
            return dbContext.Set<T>().AsQueryable();
        }

        public async Task DeleteAsync(int id)
        {
            T? entity = await dbContext.Set<T>().FindAsync(id);

            if (entity != null)
            {
                dbContext.Set<T>().Remove(entity);
            }
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await dbContext.Set<T>().ToListAsync();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await dbContext.Set<T>().FindAsync(id);
        }

        public async Task SaveChangesAsync()
        {
            await dbContext.SaveChangesAsync();
        }

        public Task UpdateAsync(T entity)
        {
            dbContext.Set<T>().Update(entity);
            return Task.CompletedTask;
        }
    }
}
