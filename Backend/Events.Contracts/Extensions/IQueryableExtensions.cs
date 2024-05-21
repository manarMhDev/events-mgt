

using Events.Contracts.Wrappers;

namespace Events.Contracts.Extensions
{
    public static class IQueryableExtensions
    {
        public static IQueryable<TSource> Page<TSource>(this IQueryable<TSource> source, int pageNumber = 1, int pageSize = 10)
        {
            pageNumber = pageNumber >= 1 ? pageNumber : 1;
            pageSize = pageSize >= 1 ? pageSize : 20;
            return source.Skip((pageNumber - 1) * pageSize).Take(pageSize);
        }

        public static PagedResult<T> ToQueryResult<T>(this IQueryable<T> dbQuery,
            int pageNumber = 1, int pageSize = 10,
            bool isCountOnly = false,
            int maxPageSize = int.MaxValue) where T : class
        {
            if (dbQuery == null)
                throw new ArgumentNullException("dbQuery");
            int count = dbQuery.Count();
            dbQuery = dbQuery.Page(pageNumber, pageSize <= maxPageSize ? pageSize : maxPageSize);
            var data = isCountOnly ? null : dbQuery;

            return PagedResult<T>.Success(data, count, pageNumber, pageSize);
        }
    }
}
