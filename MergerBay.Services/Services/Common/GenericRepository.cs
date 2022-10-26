using MergerBay.Domain.Context;
using MergerBay.Infrastructure.Interfaces.Common;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace MergerBay.Services.Services.Common
{
    public class GenericRepository : IGenericRepository
    {
        protected readonly MergerBayContext Context;

        public GenericRepository(MergerBayContext _Context)
        {
            Context = _Context;
        }
        public async Task AddAsync<T>(T entity) where T : class
        {
            await Context.Set<T>().AddAsync(entity);
        }

        public async Task AddRangeAsync<T>(IEnumerable<T> entities) where T : class
        {
            await Context.Set<T>().AddRangeAsync(entities);
        }
        public async Task<IEnumerable<T>> GetAllAsync<T>() where T : class
        {
            return await Context.Set<T>().AsNoTracking().ToListAsync();
        }
        public async ValueTask<T> GetByIdAsync<T>(Guid id) where T : class
        {
            return await Context.Set<T>().FindAsync(id);
        }

        public void Remove<T>(T entity) where T : class
        {
            Context.Set<T>().Remove(entity);
        }

        public void RemoveRange<T>(IEnumerable<T> entities) where T : class
        {
           Context.Set<T>().RemoveRange(entities);
        }
        public async Task<T> FirstOrDefaultAsync<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            return await Context.Set<T>().FirstOrDefaultAsync(predicate);
        }
        public async Task<IEnumerable<T>> Where<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            return await Context.Set<T>().Where(predicate).ToListAsync();
        }

        public async Task<int> CommitChangesAsync()
        {
            return await Context.SaveChangesAsync();
        }

        public async Task Update<T>(T entity) where T : class
        {
            await Task.FromResult(Context.Update(entity));
 ;      }
        public async Task<bool> Any<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            return await Context.Set<T>().AnyAsync(predicate);
        }
    }
}
