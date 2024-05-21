

using Events.Application.Common.Interfaces.Authentication;
using Events.Application.SeatsTypes.Queries;
using Events.Contracts.Extensions;
using Events.Contracts.Wrappers;
using Events.Domain.Entities;
using Events.Domain.Enums;
using Events.Infrastructure.UnitOfWork;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using static Events.Application.Seats.Queries.GetSeats;

namespace Events.Application.Seats.Queries
{
    public class GetSeats : IRequest<Response<PagedResult<GetSeatsDto>>>
    {
        private readonly int _page;
        private readonly int _size;
        private readonly int _eventId;
        public GetSeats(int eventId,int page, int size)
        {
            _page = page;
            _size = size;
            _eventId = eventId;
        }
        public class GetSeatsHandler : BaseHandler, IRequestHandler<GetSeats, Response<PagedResult<GetSeatsDto>>>
        {
            public GetSeatsHandler(IUnitOfWork iUnitOfWork, IJwtTokenGenerator jwtTokenGenerator, 
                UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor) : base(iUnitOfWork, jwtTokenGenerator, userManager, httpContextAccessor)
            {
            }

            public async Task<Response<PagedResult<GetSeatsDto>>> Handle(GetSeats request, CancellationToken cancellationToken)
            {
                var eve = await _unitOfWork.EventRepository.GetById(request._eventId);
                var result = await _unitOfWork.PlaceSeatRepository.SearchFor(x=>x.EventPlaceId == eve.EventPlaceId, "EventPlace,SeatType").ToListAsync();
                var lis = new List<GetSeatsDto>();
                for(int i = 0 ; i < result.Count; i++)
                {
                    var obj = new GetSeatsDto
                    {
                        Id = result[i].Id,
                        Code = result[i].Code,
                        EventPlaceName = result[i].EventPlace.Name,
                        SeatTypeName = result[i].SeatType.Name,
                        IsTaken = _unitOfWork.EventSeatRepository
                                              .SearchFor(x => x.PlaceSeatId == result[i].Id && x.Invitation.EventId == request._eventId, "Invitation")
                                              .FirstOrDefault() == null ? false : true,
                    };

                    lis.Add(obj);
                };
                var res = lis.AsQueryable().ToQueryResult(request._page, request._size);
                return new Response<PagedResult<GetSeatsDto>>(res, true);
            }
        }
        public class GetSeatsDto
        {
            public int Id { get; set; }
            public string Code { get; set; }
            public string EventPlaceName { get; set; }
            public string SeatTypeName { get; set; }
            public bool IsTaken { get; set; }
        }
    }
}
