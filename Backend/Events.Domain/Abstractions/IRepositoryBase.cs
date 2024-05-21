

using System.Linq.Expressions;

namespace Events.Infrastructure.Repositories
{
    public interface IRepositoryBase<T>
    {
        IQueryable<T> SearchFor(Expression<Func<T, bool>> predicate = null, string Include = null, bool hasTracking = true);
        Task<T> GetById(int id, bool isTracking = true);
        void Insert(T entity);
        void Delete(T entity);
        void UpdateEntity(T entity);
    }
}
