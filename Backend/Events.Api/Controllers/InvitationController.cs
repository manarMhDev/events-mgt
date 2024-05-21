
using Events.Application.EventPlaces.Queries.GetPlaces;
using Events.Application.Invitations.Commands;
using Events.Application.Invitations.Queries;
using Events.Application.PersonType.Commands;
using Events.Contracts.Wrappers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Events.Application.EventPlaces.Queries.GetPlaces.GetEventPlaceById;
using static Events.Application.EventPlaces.Queries.GetPlaces.GetEventPlaces;
using static Events.Application.Invitations.Commands.CreateInvitation;
using static Events.Application.Invitations.Queries.GetInvitationById;
using static Events.Application.Invitations.Queries.GetInvitations;
using static Events.Application.PersonType.Commands.UpdateInvitation;
using static Events.Application.PersonType.Commands.UpdatePersonType;

namespace Events.Api.Controllers
{
    [ApiController]
    [Route("invitation")]
    [Authorize]
    public class InvitationController : Controller
    {
        private readonly IMediator _mediator;

        public InvitationController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("create-one")]
        public async Task<Response<bool>> CreateInvitation(InvitationCreateDto request)
        {
            var result = await _mediator.Send(new CreateInvitation(request));
            return result;
        }
        [HttpGet("get-all")]
        public async Task<Response<PagedResult<GetInvitationsDto>>> GetAll(int page = 1, int size = 10)
        {
            var result = await _mediator.Send(new GetInvitations(page, size));
            return result;
        }
        [HttpGet("get-one")]
        public async Task<Response<InvitationDto>> GetInvitationById(int id)
        {
            var result = await _mediator.Send(new GetInvitationById(id));
            return result;
        }
        [HttpPatch("edit-one")]
        public async Task<Response<bool>> UpdatInvitation(UpdateInvitationDto request)
        {
            var result = await _mediator.Send(new UpdateInvitation(request));
            return result;
        }
        [HttpDelete("delete-one")]
        public async Task<Response<bool>> DeleteInvitation(int id)
        {
            var result = await _mediator.Send(new DeleteInvitation(id));
            return result;
        }
    }
}
