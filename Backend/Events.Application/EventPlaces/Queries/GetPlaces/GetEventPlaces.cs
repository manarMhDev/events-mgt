

using Events.Application.Common.Interfaces.Authentication;
using Events.Contracts.Extensions;
using Events.Contracts.Wrappers;
using Events.Domain.Entities;
using Events.Domain.Enums;
using Events.Infrastructure.UnitOfWork;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using static Events.Application.EventPlaces.Queries.GetPlaces.GetEventPlaces;

namespace Events.Application.EventPlaces.Queries.GetPlaces
{
    public class GetEventPlaces : IRequest<Response<PagedResult<EventPlaceDto>>>
    {
        private readonly int _page;
        private readonly int _size;
        public GetEventPlaces(int page, int size)
        {
            _page = page;
            _size = size;
        }
        public class GetEventPlacesHandler : BaseHandler, IRequestHandler<GetEventPlaces, Response<PagedResult<EventPlaceDto>>>
        {
            public GetEventPlacesHandler(IUnitOfWork iUnitOfWork, IJwtTokenGenerator jwtTokenGenerator,
                UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor) : base(iUnitOfWork, jwtTokenGenerator, userManager, httpContextAccessor)
            {
            }

            public async Task<Response<PagedResult<EventPlaceDto>>> Handle(GetEventPlaces request, CancellationToken cancellationToken)
            {
               var result =  _unitOfWork.EventPlaceRepository.SearchFor().Select(item => 
                new EventPlaceDto
                {
                     Id = item.Id,
                      Name = item.Name,
                        SeatingChartImagePath = item.SeatingChartImagePath,
                         Columns = item.Columns,
                          Rows = item.Rows,
                           SeatingChart = item.SeatingChart
                }).ToQueryResult(request._page, request._size);
                return new Response<PagedResult<EventPlaceDto>>(result, true);
            }
        }
        public class EventPlaceDto
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public SeatingChart SeatingChart { get; set; }
            public string SeatingChartImagePath { get; set; }
            public int? Columns { get; set; }
            public int? Rows { get; set; }
        }

    }
}
