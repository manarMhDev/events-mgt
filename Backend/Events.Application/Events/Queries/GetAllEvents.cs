

using Events.Application.Common.Interfaces.Authentication;
using Events.Contracts.Wrappers;
using Events.Domain.Entities;
using Events.Infrastructure.UnitOfWork;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using static Events.Application.EventPlaces.Queries.GetPlaces.GetEventPlaces;
using static Events.Application.PersonType.Queries.GetEvents;

namespace Events.Application.EventPlaces.Queries.GetPlaces
{
    public class GetAllEvents : IRequest<Response<List<GetEventsDto>>>
    {
        public GetAllEvents()
        {
        }
        public class GetAllEventsHandler : BaseHandler, IRequestHandler<GetAllEvents, Response<List<GetEventsDto>>>
        {
            public GetAllEventsHandler(IUnitOfWork iUnitOfWork, IJwtTokenGenerator jwtTokenGenerator,
                UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor) : base(iUnitOfWork, jwtTokenGenerator, userManager, httpContextAccessor)
            {
            }

            public async Task<Response<List<GetEventsDto>>> Handle(GetAllEvents request, CancellationToken cancellationToken)
            {
                var result = await _unitOfWork.EventRepository.SearchFor().Select(item =>
                 new GetEventsDto
                 {
                     Id = item.Id,
                      NameArabic = item.NameArabic,
                       NameEnglish = item.NameEnglish
                 }).ToListAsync();
                return new Response<List<GetEventsDto>>(result,true);
            }
        }
     

    }
}
