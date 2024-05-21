

using Events.Application.Common.Interfaces.Authentication;
using Events.Contracts.Services.Interfaces;
using Events.Contracts.Wrappers;
using Events.Domain.Entities;
using Events.Domain.Enums;
using Events.Infrastructure.UnitOfWork;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Events.Application.Seats.Commands
{
    public class BookASeat : IRequest<Response<bool>>
    {
        private readonly BookASeatDto _bookASeatDto;
        public BookASeat(BookASeatDto bookASeatDto)
        {

            _bookASeatDto = bookASeatDto;

        }
        public class BookASeatHandler : BaseHandler, IRequestHandler<BookASeat, Response<bool>>
        {
            public BookASeatHandler(IUnitOfWork iUnitOfWork,
                IJwtTokenGenerator jwtTokenGenerator,
                UserManager<ApplicationUser> userManager,
                IHttpContextAccessor httpContextAccessor) : base(iUnitOfWork, jwtTokenGenerator, userManager, httpContextAccessor)
            {
            }

            public async Task<Response<bool>> Handle(BookASeat request, CancellationToken cancellationToken)
            {
                var seat = await _unitOfWork.EventSeatRepository
                    .SearchFor(x => x.InvitationId == request._bookASeatDto.InvitationId)
                    .FirstOrDefaultAsync();
                if(seat is null)
                {
                    seat = new EventSeat(request._bookASeatDto.InvitationId, request._bookASeatDto.SeatPlaceId);
                    _unitOfWork.EventSeatRepository.Insert(seat);
                    await _unitOfWork.CommitAsync();
                    var invitation = await _unitOfWork.InvitationRepository.GetById(request._bookASeatDto.InvitationId);
                    invitation.BookASeat(seat.Id);
                    return await _unitOfWork.CommitAsync();
                }
                else
                {
                    seat.UpdateEventSeat(request._bookASeatDto.SeatPlaceId);
                    return await _unitOfWork.CommitAsync();
                }
            }
        }
        public class BookASeatDto
        {
            public int InvitationId { get; set; }
            public int SeatPlaceId { get; set; }
        }
    }
}
