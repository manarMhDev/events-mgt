
using System.Net;

namespace Events.Contracts.Wrappers
{
    public class PagedResult<T> : Response
    {
        public PagedResult() 
        {

        }

        public IEnumerable<T> Data { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int TotalCount { get; set; }
        public int PageSize { get; set; }
        public List<string> Messages { get; set; }
        public bool HasPreviousPage => CurrentPage > 1;
        public bool HasNextPage => CurrentPage < TotalPages;

        public PagedResult(List<T> data, int count = 0, int page = 1, int pageSize = 50) : this(true, data, null, count, page = 1,
            pageSize, HttpStatusCode.OK)
        {
            Data = data;
        }

        internal PagedResult(bool succeeded, IEnumerable<T> data = default, List<string> messages = null, int count = 0, int page = 1,
            int pageSize = 50, HttpStatusCode StatusCode = HttpStatusCode.OK)
        {
            Data = data;
            CurrentPage = page;
            Succeeded = succeeded;
            PageSize = pageSize;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            TotalCount = count;
            Messages = messages;
            base.StatusCode = (int)StatusCode;
        }

        public static PagedResult<T> Success(IEnumerable<T> data, int count, int page, int pageSize)
        {
            return new PagedResult<T>(true, data, null, count, page, pageSize);
        }

        public static PagedResult<T> Failure(List<string> messages, HttpStatusCode StatusCode = HttpStatusCode.BadRequest)
        {
            return new PagedResult<T>(false, default, messages, StatusCode: StatusCode);
        }
    }
}
