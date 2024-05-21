

using Events.Application.Common.Interfaces.Authentication;
using Events.Application.EventPlaces.Commands.Update;
using Events.Contracts.Services.Interfaces;
using Events.Contracts.Wrappers;
using Events.Domain.Entities;
using Events.Domain.Enums;
using Events.Infrastructure.UnitOfWork;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Events.Application.Events.Commands
{
    public class UpdateEvent : IRequest<Response<bool>>
    {
        private readonly UpdateEventDto _updateEventDto;
        public UpdateEvent(UpdateEventDto updateEventDto)
        {

            _updateEventDto = updateEventDto;

        }
        public class UpdateEventHandler : BaseHandler, IRequestHandler<UpdateEvent, Response<bool>>
        {
            private readonly IFileService _documentService;
            public UpdateEventHandler(IUnitOfWork iUnitOfWork,
                IJwtTokenGenerator jwtTokenGenerator,
                UserManager<ApplicationUser> userManager,
                IHttpContextAccessor httpContextAccessor,
                IFileService documentService) : base(iUnitOfWork, jwtTokenGenerator, userManager, httpContextAccessor)
            {
                _documentService = documentService;
            }

            public async Task<Response<bool>> Handle(UpdateEvent request, CancellationToken cancellationToken)
            {
                var obj = await _unitOfWork.EventRepository.GetById(request._updateEventDto.Id);
              
                if (obj is not null)
                    obj.UpdateEvent(request._updateEventDto.NameArabic,
                        request._updateEventDto.NameEnglish,
                        request._updateEventDto.Description,
                        request._updateEventDto.EventDate,
                        request._updateEventDto.EventPlaceId);
                return await _unitOfWork.CommitAsync();
            }
        }
        public class UpdateEventDto
        {
            public int Id { get; set; }
            public string NameArabic { get; set; }
            public string NameEnglish { get; set; }
            public string Description { get; set; }
            public DateTime EventDate { get; set; }
            public int EventPlaceId { get; set; }
        }
    }
}