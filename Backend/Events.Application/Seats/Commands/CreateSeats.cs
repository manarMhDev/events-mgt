

using Events.Application.Common.Interfaces.Authentication;
using Events.Application.SeatsTypes.Commands;
using Events.Contracts.Services.Interfaces;
using Events.Contracts.Wrappers;
using Events.Domain.Entities;
using Events.Domain.Enums;
using Events.Infrastructure.UnitOfWork;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Events.Application.Seats.Commands
{
    public class CreateSeats : IRequest<Response<bool>>
    {
        private readonly CreateSeatsDto _createSeatsDto;
        public CreateSeats(CreateSeatsDto createSeatsDto)
        {

            _createSeatsDto = createSeatsDto;

        }
        public class CreateSeatsHandler : BaseHandler, IRequestHandler<CreateSeats, Response<bool>>
        {
            private readonly IFileService _documentService;
            public CreateSeatsHandler(IUnitOfWork iUnitOfWork,
                IJwtTokenGenerator jwtTokenGenerator,
                UserManager<ApplicationUser> userManager,
                IHttpContextAccessor httpContextAccessor) : base(iUnitOfWork, jwtTokenGenerator, userManager, httpContextAccessor)
            {
            }

            public async Task<Response<bool>> Handle(CreateSeats request, CancellationToken cancellationToken)
            {
                for(int i = request._createSeatsDto.Start;i< (request._createSeatsDto.Start+request._createSeatsDto.Count); i++)
                {
                    var seat = new PlaceSeat(
                                     request._createSeatsDto.EventPlaceId,
                                     request._createSeatsDto.Prefix.ToString() + i.ToString(),
                                     request._createSeatsDto.SeatTypeId);
                    _unitOfWork.PlaceSeatRepository.Insert(seat);

          
                }
                return await _unitOfWork.CommitAsync();

            }
        }
        public class CreateSeatsDto
        {
            public int EventPlaceId { get; set; }
            public int SeatTypeId { get; set; }
            public Prefixes Prefix { get; set; }
            public int Start { get; set; }
            public int Count { get; set; }
        }
    }
}
