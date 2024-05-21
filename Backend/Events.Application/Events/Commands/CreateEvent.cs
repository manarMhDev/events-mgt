

using Events.Application.Common.Interfaces.Authentication;
using Events.Contracts.Wrappers;
using Events.Domain.Entities;
using Events.Domain.Enums;
using Events.Infrastructure.UnitOfWork;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Events.Application.PersonType.Commands
{
    public class CreateEvent : IRequest<Response<bool>>
    {
        private readonly CreateEventDto _CreateEventDto;
        public CreateEvent(CreateEventDto CreateEventDto)
        {

            _CreateEventDto = CreateEventDto;

        }
        public class CreateEventTypeHandler : BaseHandler, IRequestHandler<CreateEvent, Response<bool>>
        {
            public CreateEventTypeHandler(IUnitOfWork iUnitOfWork, 
                IJwtTokenGenerator jwtTokenGenerator, 
                UserManager<ApplicationUser> userManager,
                IHttpContextAccessor httpContextAccessor) : base(iUnitOfWork, jwtTokenGenerator, userManager, httpContextAccessor)
            {
            }

            public async Task<Response<bool>> Handle(CreateEvent request, CancellationToken cancellationToken)
            {
                var obj = new Event(
                      request._CreateEventDto.NameArabic,
                       request._CreateEventDto.NameEnglish,
                        request._CreateEventDto.Description,
                         request._CreateEventDto.EventDate,
                          request._CreateEventDto.EventPlaceId);
                _unitOfWork.EventRepository.Insert(obj);

                return await _unitOfWork.CommitAsync();
            }
        }
        public class CreateEventDto
        {
            public string NameArabic { get;  set; }
            public string NameEnglish { get;  set; }
            public string? Description { get;  set; }
            public int EventPlaceId { get;  set; }
            public DateTime EventDate { get;  set; }
        }
    }
}
