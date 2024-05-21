
using Events.Application.Common.Interfaces.Authentication;
using Events.Application.SeatsTypes.Queries;
using Events.Contracts.Wrappers;
using Events.Domain.Entities;
using Events.Domain.Enums;
using Events.Infrastructure.UnitOfWork;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using static Events.Application.Seats.Queries.GetNonTakenSeats;

namespace Events.Application.Seats.Queries
{
    public class GetNonTakenSeats : IRequest<Response<List<AllSeatsDto>>>
    {
        private readonly AllSeatsRequestDto _allSeatsRequestDto;
        public GetNonTakenSeats(AllSeatsRequestDto allSeatsRequestDto)
        {
            _allSeatsRequestDto = allSeatsRequestDto;
        }
        public class GetAllSeatsHandler : BaseHandler, IRequestHandler<GetNonTakenSeats, Response<List<AllSeatsDto>>>
        {
            public GetAllSeatsHandler(IUnitOfWork iUnitOfWork, IJwtTokenGenerator jwtTokenGenerator,
                UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor) : base(iUnitOfWork, jwtTokenGenerator, userManager, httpContextAccessor)
            {
            }

            public async Task<Response<List<AllSeatsDto>>> Handle(GetNonTakenSeats request, CancellationToken cancellationToken)
            {
                var eve = await _unitOfWork.EventRepository.GetById(request._allSeatsRequestDto.EventId);
                var taken = await  _unitOfWork.EventSeatRepository
                    .SearchFor(x => x.Invitation.EventId == request._allSeatsRequestDto.EventId, "Invitation")
                    .Select(x => x.PlaceSeatId).ToListAsync();

                var result = await _unitOfWork.PlaceSeatRepository
                    .SearchFor(x=> x.EventPlaceId == eve.EventPlaceId
                    && !taken.Contains(x.Id), "SeatType")
                    .Select(item => new AllSeatsDto
                 {
                     Id = item.Id,
                     Code = item.Code,
                     SeatType = item.SeatType.Name,
                 }).ToListAsync();
                return new Response<List<AllSeatsDto>>(result, true);
            }
        }
        public class AllSeatsDto
        {
            public int Id { get; set; }
            public string Code { get; set; }
            public string SeatType { get; set; }

        }
        public class AllSeatsRequestDto
        {
            public int EventId { get; set; }

        }

    }
}
