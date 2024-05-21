
using Events.Application.EventPlaces.Queries.GetPlaces;
using Events.Application.TitleSecond.Commands;
using Events.Application.TitleSecond.Queries;
using Events.Contracts.Dtos;
using Events.Contracts.Wrappers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Events.Application.TitleFirst.Queries.GetTitleFirst;
using static Events.Application.TitleSecond.Commands.CreateTitleSecond;
using static Events.Application.TitleSecond.Commands.UpdateTitleSecond;
using static Events.Application.TitleSecond.Queries.GetTitleSecond;
using static Events.Application.TitleSecond.Queries.GetTitleSecondById;

namespace Events.Api.Controllers
{
    [ApiController]
    [Route("second-title")]
    [Authorize]
    public class SecondTitleController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SecondTitleController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("create-one")]
        public async Task<Response<bool>> CreateSecondTitle(CreateSecondTitleDto request)
        {
            var result = await _mediator.Send(new CreateTitleSecond(request));
            return result;
        }
        [HttpGet("get-all")]
        public async Task<Response<PagedResult<GetTitleSecondDto>>> GetSecondTitles(int page = 1, int size = 10)
        {
            var result = await _mediator.Send(new GetTitleSecond(page,size));
            return result;
        }
        [HttpGet("get-all-data")]
        public async Task<Response<List<GetTitleSecondDto>>> GetAllData()
        {
            var result = await _mediator.Send(new GetAllTitles2());
            return result;
        }
        [HttpGet("get-one")]
        public async Task<Response<TitleSecondDto>> GetSecondTitleById(int id)
        {
            var result = await _mediator.Send(new GetTitleSecondById(id));
            return result;
        }
        [HttpPatch("edit-one")]
        public async Task<Response<bool>> UpdateSecondTitle(UpdateTitleSecondDto request)
        {
            var result = await _mediator.Send(new UpdateTitleSecond(request));
            return result;
        }
        [HttpDelete("delete-one")]
        public async Task<Response<bool>> DeleteSecondTitle(int id)
        {
            var result = await _mediator.Send(new DeleteTitleSecond(id));
            return result;
        }
    }
}
