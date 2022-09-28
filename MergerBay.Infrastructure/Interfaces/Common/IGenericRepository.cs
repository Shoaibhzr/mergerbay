using MergerBay.Domain.Entities.Sectors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MergerBay.Infrastructure.Interfaces.Common
{
    public interface IGenericRepository
    {
        ValueTask<T> GetByIdAsync<T>(int id) where T : class;
        Task<T> FirstOrDefaultAsync<T>(Expression<Func<T, bool>> predicate) where T : class;
        Task AddAsync<T>(T entity) where T : class;
        Task AddRangeAsync<T>(IEnumerable<T> entities) where T : class;
        void Remove<T>(T entity) where T : class;
        void RemoveRange<T>(IEnumerable<T> entities) where T:class;
        IEnumerable<T> Where<T>(Expression<Func<T, bool>> predicate) where T : class;
        Task<int> CommitChangesAsync();
        //Task<List<Sectors>> GetAllAsync();
        Task<IEnumerable<T>> GetAllAsync<T>() where T : class;
    }
}
