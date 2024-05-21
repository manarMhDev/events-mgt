

using Events.Application.Common.Interfaces.Authentication;
using Events.Contracts.Wrappers;
using Events.Domain.Entities;
using Events.Domain.Enums;
using Events.Infrastructure.UnitOfWork;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using static Events.Application.SeatsTypes.Queries.GetSeatsTypeById;

namespace Events.Application.SeatsTypes.Queries
{
    public class GetSeatsTypeById : IRequest<Response<SeatsTypeDto>>
    {
        private readonly int _id;
        public GetSeatsTypeById(int id)
        {

            _id = id;

        }
        public class GetSeatsTypesByIdHandler : BaseHandler, IRequestHandler<GetSeatsTypeById, Response<SeatsTypeDto>>
        {
            public GetSeatsTypesByIdHandler(IUnitOfWork iUnitOfWork, IJwtTokenGenerator jwtTokenGenerator, UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor) : base(iUnitOfWork, jwtTokenGenerator, userManager, httpContextAccessor)
            {
            }

            public async Task<Response<SeatsTypeDto>> Handle(GetSeatsTypeById request, CancellationToken cancellationToken)
            {
                var obj = await _unitOfWork.SeatsTypeRepository.GetById(request._id);
                var model = new SeatsTypeDto();
                model.Id = obj.Id;
                model.Name = obj.Name;
                model.Color = obj.Color;
                model.ColorText = obj.ColorText;
                model.ImagePath = obj.ImagePath;
                
                return new Response<SeatsTypeDto>(model);
            }
        }
        public class SeatsTypeDto
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Color { get; set; }
            public string ColorText { get; set; }
            public string ImagePath { get; set; }

        }
    }
}
