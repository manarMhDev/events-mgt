using Events.Application.EventPlaces.Commands.CreatePlace;
using Events.Application.EventPlaces.Commands.Delete;
using Events.Application.EventPlaces.Commands.Update;
using Events.Application.EventPlaces.Queries.GetPlaces;
using Events.Contracts.Wrappers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Events.Application.EventPlaces.Commands.CreatePlace.CreateEventPlace;
using static Events.Application.EventPlaces.Commands.Update.UpdateEventPlace;
using static Events.Application.EventPlaces.Queries.GetPlaces.GetEventPlaceById;
using static Events.Application.EventPlaces.Queries.GetPlaces.GetEventPlaces;

namespace Events.Api.Controllers
{
    [ApiController]
    [Route("event-place")]
    [Authorize]
    public class EventPlaceController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EventPlaceController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("create-one")]
        public async Task<Response<bool>> CreateEventPlace([FromForm]EventPlaceCreate request)
        {
            var result = await _mediator.Send(new CreateEventPlace(request));
            return result;
        }
        [HttpGet("get-all-data")]
        public async Task<Response<List<EventPlaceDto>>> GetAllData()
        {
            var result = await _mediator.Send(new GetAllEventPlaces());
            return result;
        }
        [HttpGet("get-all")]
        public async Task<Response<PagedResult<EventPlaceDto>>> GetAll(int page = 1, int size = 10)
        {
            var result = await _mediator.Send(new GetEventPlaces(page, size));
            return result;
        }
        [HttpGet("get-one")]
        public async Task<Response<GetEventPlaceDto>> GetEventPlaceById(int id)
        {
            var result = await _mediator.Send(new GetEventPlaceById(id));
            return result;
        }
        [HttpPatch("edit-one")]
        public async Task<Response<bool>> UpdateEventPlace([FromForm] UpdateEventPlaceDto request)
        {
            var result = await _mediator.Send(new UpdateEventPlace(request));
            return result;
        }
        [HttpDelete("delete-one")]
        public async Task<Response<bool>> DeleteEventPlace(int id)
        {
            var result = await _mediator.Send(new DeleteEventPlace(id));
            return result;
        }
    }
}
