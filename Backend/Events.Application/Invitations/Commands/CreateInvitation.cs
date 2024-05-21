

using Events.Application.Common.Interfaces.Authentication;
using Events.Application.TitleSecond.Commands;
using Events.Contracts.Wrappers;
using Events.Domain.Entities;
using Events.Domain.Enums;
using Events.Infrastructure.UnitOfWork;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Events.Application.Invitations.Commands
{
    public class CreateInvitation : IRequest<Response<bool>>
    {
        private readonly InvitationCreateDto _invitationCreateDto;
        public CreateInvitation(InvitationCreateDto invitationCreateDto)
        {

            _invitationCreateDto = invitationCreateDto;

        }
        public class InvitationCreateDtoHandler : BaseHandler, IRequestHandler<CreateInvitation, Response<bool>>
        {
            public InvitationCreateDtoHandler(IUnitOfWork iUnitOfWork,
                IJwtTokenGenerator jwtTokenGenerator,
                UserManager<ApplicationUser> userManager,
                IHttpContextAccessor httpContextAccessor) : base(iUnitOfWork, jwtTokenGenerator, userManager, httpContextAccessor)
            {
            }

            public async Task<Response<bool>> Handle(CreateInvitation request, CancellationToken cancellationToken)
            {
                var obj = new Invitation(
                      request._invitationCreateDto.TitleFirstId,
                      request._invitationCreateDto.TitleSecondId == 0 ? null : request._invitationCreateDto.TitleSecondId,
                      request._invitationCreateDto.PersonTypeId,
                      request._invitationCreateDto.EventId,
                      request._invitationCreateDto.FullName,
                      request._invitationCreateDto.Email,
                      request._invitationCreateDto.SendWhatsapp,
                      request._invitationCreateDto.HasCameToEvent,
                      request._invitationCreateDto.SendEmail,
                      request._invitationCreateDto.Language,
                      request._invitationCreateDto.InvitationStatus,
                      request._invitationCreateDto.FormType,
                      request._invitationCreateDto.Whatsapp,
                      request._invitationCreateDto.Phone,
                      request._invitationCreateDto.Position,
                      request._invitationCreateDto.Party);
                _unitOfWork.InvitationRepository.Insert(obj);

                return await _unitOfWork.CommitAsync();
            }
        }
        public class InvitationCreateDto
        {
            public int EventId { get;  set; }
            public int TitleFirstId { get;  set; }
            public int TitleSecondId { get;  set; }
            public int PersonTypeId { get;  set; }
            public string FullName { get;  set; }
            public string Email { get;  set; }
            public string? Whatsapp { get;  set; } = null;
            public string? Phone { get;  set; } = null;
            public string? Position { get;  set; } = null;
            public string? Party { get;  set; } = null;
            public bool SendWhatsapp { get;  set; }
            public bool HasCameToEvent { get;  set; }
            public bool SendEmail { get;  set; }
            public Language Language { get;  set; }
            public InvitationStatus InvitationStatus { get;  set; }
            public FormType FormType { get;  set; }
        }
    }
}
