

namespace Events.Contracts.Dtos
{
    public  class PagintationRequest
    {
        public int PageSize { get; set; } = 20;
        public int PageNumber { get; set; } = 1;
    }
}
