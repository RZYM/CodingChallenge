using CodingChallengeAPI.Core.IRepositories;
using CodingChallengeAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace CodingChallengeAPI.Core.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected ApplicationDbContext _context;
        protected DbSet<T> _dbSet;
        protected readonly ILogger _logger;

        public GenericRepository(ApplicationDbContext context, ILogger logger)
        {
            _context = context;
            _logger = logger;
            this._dbSet = _context.Set<T>();
        }

        public virtual async Task<IEnumerable<T>> All()
        {
            return await _dbSet.ToListAsync();
        }

        public virtual async Task<T> GetById(long id)
        {
            return await _dbSet.FindAsync(id);
        }

        public virtual async Task<bool> Add(T entity)
        {
            await _dbSet.AddAsync(entity);
            return true;
        }

        public virtual Task<bool> Update(T entity)
        {
            throw new NotImplementedException();
        }

        public virtual Task<bool> Delete(long id)
        {
            throw new NotImplementedException();
        }
    }
}
