

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
    public class UpdateInvitation : IRequest<Response<bool>>
    {
        private readonly UpdateInvitationDto _updateInvitationDto;
        public UpdateInvitation(UpdateInvitationDto updatePInvitationeDto)
        {

            _updateInvitationDto = updatePInvitationeDto;

        }
        public class UpdateInvitationHandler : BaseHandler, IRequestHandler<UpdateInvitation, Response<bool>>
        {
            public UpdateInvitationHandler(IUnitOfWork iUnitOfWork, IJwtTokenGenerator jwtTokenGenerator, UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor) : base(iUnitOfWork, jwtTokenGenerator, userManager, httpContextAccessor)
            {
            }

            public async Task<Response<bool>> Handle(UpdateInvitation request, CancellationToken cancellationToken)
            {
                var obj = await _unitOfWork.InvitationRepository.GetById(request._updateInvitationDto.Id);
                if(obj is not null)
                obj.UpdateInvitation(
                      request._updateInvitationDto.TitleFirstId,
                      request._updateInvitationDto.TitleSecondId == 0 ? null : request._updateInvitationDto.TitleSecondId ,
                      request._updateInvitationDto.PersonTypeId,
                      request._updateInvitationDto.EventId,
                      request._updateInvitationDto.FullName,
                      request._updateInvitationDto.Email,
                      request._updateInvitationDto.SendWhatsapp,
                      request._updateInvitationDto.HasCameToEvent,
                      request._updateInvitationDto.SendEmail,
                      request._updateInvitationDto.Language,
                      request._updateInvitationDto.InvitationStatus,
                      request._updateInvitationDto.FormType,
                      request._updateInvitationDto.Whatsapp,
                      request._updateInvitationDto.Phone,
                      request._updateInvitationDto.Position,
                      request._updateInvitationDto.Party);
                return await _unitOfWork.CommitAsync();
            }
        }
        public class UpdateInvitationDto
        {
            public int Id { get; set; }
            public int EventId { get; set; }
            public int TitleFirstId { get; set; }
            public int TitleSecondId { get; set; }
            public int PersonTypeId { get; set; }
            public string FullName { get; set; }
            public string Email { get; set; }
            public string? Whatsapp { get; set; } = null;
            public string? Phone { get; set; } = null;
            public string? Position { get; set; } = null;
            public string? Party { get; set; } = null;
            public bool SendWhatsapp { get; set; }
            public bool HasCameToEvent { get; set; }
            public bool SendEmail { get; set; }
            public Language Language { get; set; }
            public InvitationStatus InvitationStatus { get; set; }
            public FormType FormType { get; set; }
        }
    }
}
