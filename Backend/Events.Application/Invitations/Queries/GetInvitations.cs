

using Events.Application.Common.Interfaces.Authentication;
using Events.Application.TitleSecond.Queries;
using Events.Contracts.Extensions;
using Events.Contracts.Wrappers;
using Events.Domain.Entities;
using Events.Domain.Enums;
using Events.Infrastructure.UnitOfWork;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using static Events.Application.Invitations.Queries.GetInvitations;

namespace Events.Application.Invitations.Queries
{
    public class GetInvitations : IRequest<Response<PagedResult<GetInvitationsDto>>>
    {
        private readonly int _page;
        private readonly int _size;
        public GetInvitations(int page, int size)
        {
            _page = page;
            _size = size;
        }
        public class GetInvitationsHandler : BaseHandler, IRequestHandler<GetInvitations, Response<PagedResult<GetInvitationsDto>>>
        {
            public GetInvitationsHandler(IUnitOfWork iUnitOfWork, IJwtTokenGenerator jwtTokenGenerator, UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor) : base(iUnitOfWork, jwtTokenGenerator, userManager, httpContextAccessor)
            {
            }

            public async Task<Response<PagedResult<GetInvitationsDto>>> Handle(GetInvitations request, CancellationToken cancellationToken)
            {
                var result = _unitOfWork.InvitationRepository.SearchFor(null, "EventSeat,EventSeat.PlaceSeat").Select(item =>
             new GetInvitationsDto
             {
                 Id = item.Id,
                 EventId = item.EventId,
                 TitleFirstId = item.TitleFirstId,
                 TitleSecondId = (int)item.TitleSecondId,
                 PersonTypeId = item.PersonTypeId,
                 FullName = item.FullName,
                 Email = item.Email,
                 Whatsapp = item.Whatsapp,
                 Phone = item.Phone,
                 Position = item.Position,
                 Party = item.Party,
                 SeatCode = item.EventSeat != null ? item.EventSeat.PlaceSeat.Code : null,
                 SendWhatsapp = item.SendWhatsapp,
                 SendEmail = item.SendEmail,
                 Language = item.Language,
                 InvitationStatus = item.InvitationStatus,
                 FormType = item.FormType,
                 CreatedAt = item.CreatedAt,
                 ConfirmAttendance = item.ConfirmAttendance
             }).ToQueryResult(request._page, request._size);
                return new Response<PagedResult<GetInvitationsDto>>(result, true);
            }
        }
        public class GetInvitationsDto
        {
            public int Id { get;  set; }
            public DateTime CreatedAt { get; set; }
            public int EventId { get;  set; }
            public int TitleFirstId { get; set; }
            public int? TitleSecondId { get;  set; }
            public int PersonTypeId { get;  set; }
            public string FullName { get;  set; }
            public string SeatCode { get; set; }
            public string Email { get;  set; }
            public string? Whatsapp { get;  set; } = null;
            public string? Phone { get;  set; } = null;
            public string? Position { get;  set; } = null;
            public string? Party { get;  set; } = null;
            public bool SendWhatsapp { get;  set; }
            public bool SendEmail { get;  set; }
            public Language Language { get;  set; }
            public InvitationStatus InvitationStatus { get;  set; }
            public FormType FormType { get;  set; }      
            public bool ConfirmAttendance { get;  set; }
        }
    }
}