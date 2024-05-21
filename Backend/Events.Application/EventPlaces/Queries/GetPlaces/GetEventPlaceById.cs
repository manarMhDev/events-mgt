

using Events.Application.Common.Interfaces.Authentication;
using Events.Application.PersonType.Queries;
using Events.Contracts.Wrappers;
using Events.Domain.Entities;
using Events.Domain.Enums;
using Events.Infrastructure.UnitOfWork;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using static Events.Application.EventPlaces.Queries.GetPlaces.GetEventPlaceById;

namespace Events.Application.EventPlaces.Queries.GetPlaces
{
    public class GetEventPlaceById : IRequest<Response<GetEventPlaceDto>>
    {
        private readonly int _id;
        public GetEventPlaceById(int id)
        {

            _id = id;

        }
        public class GetEventPlaceByIdHandler : BaseHandler, IRequestHandler<GetEventPlaceById, Response<GetEventPlaceDto>>
        {
            public GetEventPlaceByIdHandler(IUnitOfWork iUnitOfWork, IJwtTokenGenerator jwtTokenGenerator,
                UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor) : base(iUnitOfWork, jwtTokenGenerator, userManager, httpContextAccessor)
            {
            }

            public async Task<Response<GetEventPlaceDto>> Handle(GetEventPlaceById request, CancellationToken cancellationToken)
            {
                var obj = await _unitOfWork.EventPlaceRepository.GetById(request._id);
                var model = new GetEventPlaceDto();
                model.Id = obj.Id;
                model.Name = obj.Name;
                model.SeatingChart = obj.SeatingChart;
                model.Columns = obj.Columns;
                model.Rows = obj.Rows;

                return new Response<GetEventPlaceDto>(model);
            }
        }
        public class GetEventPlaceDto
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public SeatingChart SeatingChart { get; set; }
            public string SeatingChartImagePath { get; set; }
            public int? Columns { get; set; }
            public int? Rows { get; set; }
        }
    }
}
