

using Events.Application.Common.Interfaces.Authentication;
using Events.Contracts.Dtos;
using Events.Contracts.Extensions;
using Events.Contracts.Wrappers;
using Events.Domain.Entities;
using Events.Domain.Enums;
using Events.Infrastructure.UnitOfWork;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using static Events.Application.PersonType.Queries.GetEvents;

namespace Events.Application.PersonType.Queries
{
    public class GetEvents : IRequest<Response<PagedResult<GetEventsDto>>>
    {
        private readonly int _page;
        private readonly int _size;
        public GetEvents(int page, int size)
        {
            _page = page;
            _size = size;
        }
        public class GetEventsHandler : BaseHandler, IRequestHandler<GetEvents, Response<PagedResult<GetEventsDto>>>
        {
            public GetEventsHandler(IUnitOfWork iUnitOfWork, IJwtTokenGenerator jwtTokenGenerator, UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor) : base(iUnitOfWork, jwtTokenGenerator, userManager, httpContextAccessor)
            {
            }

            public async Task<Response<PagedResult<GetEventsDto>>> Handle(GetEvents request, CancellationToken cancellationToken)
            {
                var result = _unitOfWork.EventRepository.SearchFor().Select(item =>
             new GetEventsDto
             {
                 Id = item.Id,
                 NameArabic = item.NameArabic,
                 NameEnglish = item.NameEnglish,
                 Description = item.Description,
                 EventDate = item.EventDate,
                 EventPlaceName = item.EventPlace.Name,
             }) .ToQueryResult(request._page, request._size);
                return new Response<PagedResult<GetEventsDto>>(result, true);
            }
        }
        public class GetEventsDto
        {
            public int Id { get;  set; }
            public string NameArabic { get;  set; }
            public string NameEnglish { get; set; }
            public string Description { get; set; }
            public DateTime EventDate { get; set; }
            public string EventPlaceName { get; set; }
        }


    }
}
