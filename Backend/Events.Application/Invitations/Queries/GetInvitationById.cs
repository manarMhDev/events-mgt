
using Events.Application.Common.Interfaces.Authentication;
using Events.Application.TitleSecond.Queries;
using Events.Contracts.Wrappers;
using Events.Domain.Entities;
using Events.Domain.Enums;
using Events.Infrastructure.UnitOfWork;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using static Events.Application.Invitations.Queries.GetInvitationById;

namespace Events.Application.Invitations.Queries
{
    public class GetInvitationById : IRequest<Response<InvitationDto>>
    {
        private readonly int _id;
        public GetInvitationById(int id)
        {

            _id = id;

        }
        public class GetInvitationByIdHandler : BaseHandler, IRequestHandler<GetInvitationById, Response<InvitationDto>>
        {
            public GetInvitationByIdHandler(IUnitOfWork iUnitOfWork, IJwtTokenGenerator jwtTokenGenerator, UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor) : base(iUnitOfWork, jwtTokenGenerator, userManager, httpContextAccessor)
            {
            }

            public async Task<Response<InvitationDto>> Handle(GetInvitationById request, CancellationToken cancellationToken)
            {
                var obj = await _unitOfWork.InvitationRepository.GetById(request._id);
                var model = new InvitationDto();
                model.Id = obj.Id;
                model.EventId = obj.EventId;
                model.TitleFirstId = obj.TitleFirstId;
                model.TitleSecondId = obj.TitleSecondId;
                model.PersonTypeId = obj.PersonTypeId;
                model.FullName = obj.FullName;
                model.Email = obj.Email;
                model.Whatsapp = obj.Whatsapp;
                model.Phone = obj.Phone;
                model.Position = obj.Position;
                model.Party = obj.Party;
                model.SendWhatsapp = obj.SendWhatsapp;
                model.SendEmail = obj.SendEmail;
                model.InvitationStatus = obj.InvitationStatus;
                model.FormType = obj.FormType;
                model.CreatedAt = obj.CreatedAt;
                model.ConfirmAttendance = obj.ConfirmAttendance;


                return new Response<InvitationDto>(model);
            }
        }
        public class InvitationDto
        {
            public int Id { get; set; }
            public DateTime CreatedAt { get; set; }
            public int EventId { get; set; }
            public int TitleFirstId { get; set; }
            public int? TitleSecondId { get; set; }
            public int PersonTypeId { get; set; }
            public string FullName { get; set; }
            public string Email { get; set; }
            public string? Whatsapp { get; set; } = null;
            public string? Phone { get; set; } = null;
            public string? Position { get; set; } = null;
            public string? Party { get; set; } = null;
            public bool SendWhatsapp { get; set; }
            public bool SendEmail { get; set; }
            public bool ConfirmAttendance { get; set; }
            public Language Language { get; set; }
            public InvitationStatus InvitationStatus { get; set; }
            public FormType FormType { get; set; }
        }
    }
}
