
using Events.Application.EventPlaces.Queries.GetPlaces;
using Events.Application.TitleFirst.Commands;
using Events.Application.TitleFirst.Queries;
using Events.Contracts.Dtos;
using Events.Contracts.Wrappers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Events.Application.EventPlaces.Queries.GetPlaces.GetEventPlaces;
using static Events.Application.TitleFirst.Commands.CreateTitleFirst;
using static Events.Application.TitleFirst.Commands.UpdateTitleFirst;
using static Events.Application.TitleFirst.Queries.GetTitleFirst;
using static Events.Application.TitleFirst.Queries.GetTitleFirstById;

namespace Events.Api.Controllers
{
    [ApiController]
    [Route("first-title")]
    [Authorize]
    public class FirstTitleController : ControllerBase
    {
        private readonly IMediator _mediator;

        public FirstTitleController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("create-one")]
        public async Task<Response<bool>> CreateFirstTitle(CreateFirstTitleDto request)
        {
            var result = await _mediator.Send(new CreateTitleFirst(request));
            return result;
        }
        [HttpGet("get-all")]
        public async Task<Response<PagedResult<GetTitleFirstDto>>> GetFirstTitles(int page =1 ,int size = 10)
        {
            var result = await _mediator.Send(new GetTitleFirst(page,size));
            return result;
        }
        [HttpGet("get-all-data")]
        public async Task<Response<List<GetTitleFirstDto>>> GetAllData()
        {
            var result = await _mediator.Send(new GetAllTitles1());
            return result;
        }
        [HttpGet("get-one")]
        public async Task<Response<TitleFirstDto>> GetFirstTitleById(int id)
        {
            var result = await _mediator.Send(new GetTitleFirstById(id));
            return result;
        }
        [HttpPatch("edit-one")]
        public async Task<Response<bool>> UpdateFirstTitle(UpdateTitleDto request)
        {
            var result = await _mediator.Send(new UpdateTitleFirst(request));
            return result;
        }
        [HttpDelete("delete-one")]
        public async Task<Response<bool>> DeleteFirstTitle(int id)
        {
            var result = await _mediator.Send(new DeleteTitleFirst(id));
            return result;
        }
    }
}
