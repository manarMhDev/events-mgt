

using Events.Application.Common.Interfaces.Authentication;
using Events.Contracts.Wrappers;
using Events.Domain.Entities;
using Events.Infrastructure.UnitOfWork;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using static Events.Application.EventPlaces.Queries.GetPlaces.GetEventPlaces;

namespace Events.Application.EventPlaces.Queries.GetPlaces
{
    public class GetAllEventPlaces : IRequest<Response<List<EventPlaceDto>>>
    {
        public GetAllEventPlaces()
        {
        }
        public class GetAllEventPlacesHandler : BaseHandler, IRequestHandler<GetAllEventPlaces, Response<List<EventPlaceDto>>>
        {
            public GetAllEventPlacesHandler(IUnitOfWork iUnitOfWork, IJwtTokenGenerator jwtTokenGenerator,
                UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor) : base(iUnitOfWork, jwtTokenGenerator, userManager, httpContextAccessor)
            {
            }

            public async Task<Response<List<EventPlaceDto>>> Handle(GetAllEventPlaces request, CancellationToken cancellationToken)
            {
                var result = await _unitOfWork.EventPlaceRepository.SearchFor().Select(item =>
                 new EventPlaceDto
                 {
                     Id = item.Id,
                     Name = item.Name,
                     SeatingChartImagePath = item.SeatingChartImagePath,
                     Columns = item.Columns,
                     Rows = item.Rows,
                     SeatingChart = item.SeatingChart
                 }).ToListAsync();
                return new Response<List<EventPlaceDto>>(result,true);
            }
        }
     

    }
}
