using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

namespace ProjectManagement.Domain.IRepository
{
    public interface IGenericRepository<T> where T : class
    {
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>> expression = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string include = "", bool trackChanges = true, CancellationToken cancellationToken = default);
        Task<T> GetSingleByCondition(Expression<Func<T, bool>> expression);
        Task<T> GetByIdAsync(Guid id);
        Task<T> InsertAsync([NotNull] T entity);
        Task<bool> UpdateAsync(T entity);
        Task<bool> DeleteAsync(T entity);
        Task<bool> DeleteWithIdAsync(Guid id);
        Task<bool> Any(Expression<Func<T, bool>> expression);
    }
}
