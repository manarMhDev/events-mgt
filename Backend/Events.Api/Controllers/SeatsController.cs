using Events.Application.Seats.Commands;
using Events.Application.Seats.Queries;
using Events.Application.SeatsTypes.Commands;
using Events.Application.SeatsTypes.Queries;
using Events.Contracts.Wrappers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Events.Application.Seats.Commands.BookASeat;
using static Events.Application.Seats.Commands.CreateSeats;
using static Events.Application.Seats.Commands.UpdateASeat;
using static Events.Application.Seats.Queries.GetNonTakenSeats;
using static Events.Application.Seats.Queries.GetSeats;
using static Events.Application.SeatsTypes.Commands.UpdateSeatsTypes;
using static Events.Application.SeatsTypes.Queries.GetSeatsTypes;

namespace Events.Api.Controllers
{
    [ApiController]
    [Route("seats")]
    [Authorize]
    public class SeatsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SeatsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("create-seats")]
        public async Task<Response<bool>> CreateSeats(CreateSeatsDto request)
        {
            var result = await _mediator.Send(new CreateSeats(request));
            return result;
        }
        [HttpGet("get-all")]
        public async Task<Response<PagedResult<GetSeatsDto>>> GetAll(int eventId,int page = 1, int size = 10 )
        {
            var result = await _mediator.Send(new GetSeats(eventId,page, size));
            return result;
        }
        [HttpDelete("delete-one")]
        public async Task<Response<bool>> DeleteASeat(int id)
        {
            var result = await _mediator.Send(new DeleteASeat(id));
            return result;
        }
        [HttpPatch("edit-one")]
        public async Task<Response<bool>> UpdateSeatsType(UpdateASeatDto request)
        {
            var result = await _mediator.Send(new UpdateASeat(request));
            return result;
        }
        [HttpPatch("book-seat")]
        public async Task<Response<bool>> BookASeat(BookASeatDto request)
        {
            var result = await _mediator.Send(new BookASeat(request));
            return result;
        }
        [HttpPatch("non-taken-seats")]
        public async Task<Response<List<AllSeatsDto>>> GetNonTakenSeats(AllSeatsRequestDto request)
        {
            var result = await _mediator.Send(new GetNonTakenSeats(request));
            return result;
        }
    }
}
