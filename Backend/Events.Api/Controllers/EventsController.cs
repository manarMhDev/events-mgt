
using Events.Application.EventPlaces.Commands.Delete;
using Events.Application.EventPlaces.Commands.Update;
using Events.Application.EventPlaces.Queries.GetPlaces;
using Events.Application.Events.Commands;
using Events.Application.Events.Queries;
using Events.Application.PersonType.Commands;
using Events.Application.PersonType.Queries;
using Events.Application.TitleSecond.Commands;
using Events.Application.TitleSecond.Queries;
using Events.Contracts.Dtos;
using Events.Contracts.Wrappers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Events.Application.EventPlaces.Commands.Update.UpdateEventPlace;
using static Events.Application.EventPlaces.Queries.GetPlaces.GetEventPlaceById;
using static Events.Application.Events.Commands.UpdateEvent;
using static Events.Application.Events.Queries.GetEventById;
using static Events.Application.PersonType.Commands.CreateEvent;
using static Events.Application.PersonType.Commands.UpdatePersonType;
using static Events.Application.PersonType.Queries.GetEvents;
using static Events.Application.PersonType.Queries.GetPersonTypeById;
using static Events.Application.TitleSecond.Commands.CreateTitleSecond;
using static Events.Application.TitleSecond.Queries.GetTitleSecond;

namespace Events.Api.Controllers
{
    [ApiController]
    [Route("events")]
    [Authorize]
    public class EventsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EventsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("create-one")]
        public async Task<Response<bool>> CreateEvent(CreateEventDto request)
        {
            var result = await _mediator.Send(new CreateEvent(request));
            return result;
        }
        [HttpGet("get-all")]
        public async Task<Response<PagedResult<GetEventsDto>>> GetEvents(int page = 1, int size = 10)
        {
            var result = await _mediator.Send(new GetEvents(page,size));
            return result;
        }
        [HttpGet("get-all-data")]
        public async Task<Response<List<GetEventsDto>>> GetAllData()
        {
            var result = await _mediator.Send(new GetAllEvents());
            return result;
        }
        [HttpGet("get-one")]
        public async Task<Response<GetEventDto>> GetEventById(int id)
        {
            var result = await _mediator.Send(new GetEventById(id));
            return result;
        }
        [HttpPatch("edit-one")]
        public async Task<Response<bool>> UpdateEvent([FromForm] UpdateEventDto request)
        {
            var result = await _mediator.Send(new UpdateEvent(request));
            return result;
        }
        [HttpDelete("delete-one")]
        public async Task<Response<bool>> DeleteEvent(int id)
        {
            var result = await _mediator.Send(new DeleteEvent(id));
            return result;
        }

    }
}
