using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using ProjectManagement.Domain.IRepository;
using ProjectManagement.Domain.Pagination;
using ProjectManagement.Infrastructure.Data;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using X.PagedList;

namespace ProjectManagement.Infrastructure.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        internal DbSet<T> _dbSet;
        private static readonly char[] separator = [','];

        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public virtual async Task<List<T>> GetAllAsync(Expression<Func<T, bool>> expression = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string include = "", bool trackChanges = true, CancellationToken cancellationToken = default)
        {
            IQueryable<T> query = _dbSet;

            if (expression != null) 
            { query = trackChanges ? query.Where(expression).AsNoTracking() : query.Where(expression).AsNoTracking(); }

            // Note I injected the char seperator as a private static field
            foreach (var item in include.Split(separator, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(item).AsNoTracking();
            }

            if (orderBy != null)
            {
                return await orderBy(query).AsNoTracking().ToListAsync(cancellationToken: cancellationToken);
            }
            else { return await query.AsNoTracking().ToListAsync(cancellationToken: cancellationToken); }
        }

        public async Task<IPagedList<T>> GetAll(RequestParams requestParams, List<string> includes = null)
        {
            IQueryable<T> query = _dbSet;
            
            if (includes != null)
            {
                foreach (var includeProperty in includes)
                {
                    query = query.Include(includeProperty);
                }
            }

            return await query.AsNoTracking().ToPagedListAsync(requestParams.PageNumber, requestParams.PageSize);
        }

        public virtual async Task<T> GetSingleByCondition(Expression<Func<T, bool>> expression)
        {
            return await _dbSet.AsNoTracking().FirstOrDefaultAsync(expression);
        }

        public virtual async Task<T> GetByIdAsync(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }

        public virtual async Task<bool> Any(Expression<Func<T, bool>> expression)
        {
            return await Task.FromResult(_dbSet.Any(expression));
        }

        public virtual async Task<T> InsertAsync([NotNull] T entity)
        {
            var entityToReturn = await _dbSet.AddAsync(entity);
            return entityToReturn.Entity;
        }
        
        public virtual async Task<bool> UpdateAsync(T entity)
        {
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;

            await Task.CompletedTask;
            return true;
        }

        public virtual async Task<bool> DeleteAsync(T entity)
        {
            _dbSet.Remove(entity);

            await Task.CompletedTask;
            return true;
        }

        public virtual async Task<bool> DeleteWithIdAsync(Guid id)
        {
            var del = await _dbSet.FindAsync(id);
            if (del != null)
            {
                _dbSet.Remove(del);  
                return true;
            }
            return false;
        }
    }
}
