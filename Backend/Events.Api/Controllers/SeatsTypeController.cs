using Events.Application.SeatsTypes.Commands;
using Events.Application.SeatsTypes.Queries;
using Events.Contracts.Dtos;
using Events.Contracts.Wrappers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Events.Application.SeatsTypes.Commands.CreateSeatsType;
using static Events.Application.SeatsTypes.Commands.UpdateSeatsTypes;
using static Events.Application.SeatsTypes.Queries.GetAllSeatTypes;
using static Events.Application.SeatsTypes.Queries.GetSeatsTypeById;
using static Events.Application.SeatsTypes.Queries.GetSeatsTypes;

namespace Events.Api.Controllers
{
    [ApiController]
    [Route("seats-types")]
    [Authorize]
    public class SeatsTypeController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SeatsTypeController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("create-one")]
        public async Task<Response<bool>> CreateSeatsType([FromForm] CreateSeatsTypeDto request)
        {
            var result = await _mediator.Send(new CreateSeatsType(request));
            return result;
        }
        [HttpGet("get-all-data")]
        public async Task<Response<List<AllSeatsTypeDto>>> GetAllData()
        {
            var result = await _mediator.Send(new GetAllSeatTypes());
            return result;
        }
        [HttpGet("get-all")]
        public async Task<Response<PagedResult<GetSeatsTypeDto>>> GetAll(int page = 1, int size = 10)
        {
            var result = await _mediator.Send(new GetSeatsTypes(page,size));
            return result;
        }
        [HttpGet("get-one")]
        public async Task<Response<SeatsTypeDto>> GetSeatsTypeById(int id)
        {
            var result = await _mediator.Send(new GetSeatsTypeById(id));
            return result;
        }
        [HttpPatch("edit-one")]
        public async Task<Response<bool>> UpdateSeatsType([FromForm] UpdateSeatsTypeDto request)
        {
            var result = await _mediator.Send(new UpdateSeatsTypes(request));
            return result;
        }
        [HttpDelete("delete-one")]
        public async Task<Response<bool>> DeleteSeatsType(int id)
        {
            var result = await _mediator.Send(new DeleteSeatTypes(id));
            return result;
        }
    }
}
