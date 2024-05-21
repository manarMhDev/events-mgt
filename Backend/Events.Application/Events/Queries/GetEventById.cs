

using Events.Application.Common.Interfaces.Authentication;
using Events.Contracts.Wrappers;
using Events.Domain.Entities;
using Events.Domain.Enums;
using Events.Infrastructure.UnitOfWork;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using static Events.Application.Events.Queries.GetEventById;

namespace Events.Application.Events.Queries
{
    public class GetEventById :  IRequest<Response<GetEventDto>>
    {
        private readonly int _id;
    public GetEventById(int id)
    {

        _id = id;

    }
    public class GetEventByIdHandler : BaseHandler, IRequestHandler<GetEventById, Response<GetEventDto>>
    {
        public GetEventByIdHandler(IUnitOfWork iUnitOfWork, IJwtTokenGenerator jwtTokenGenerator,
            UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor) : base(iUnitOfWork, jwtTokenGenerator, userManager, httpContextAccessor)
        {
        }

        public async Task<Response<GetEventDto>> Handle(GetEventById request, CancellationToken cancellationToken)
        {
            var obj = await _unitOfWork.EventRepository.GetById(request._id);
            var model = new GetEventDto();
            model.Id = obj.Id;
            model.NameArabic = obj.NameArabic;
            model.NameEnglish = obj.NameEnglish;
            model.Description = obj.Description;
            model.EventDate = obj.EventDate;
            model.EventPlaceId = obj.EventPlaceId;

            return new Response<GetEventDto>(model);
        }
    }
    public class GetEventDto
    {
            public int Id { get; set; }
            public string NameArabic { get; set; }
            public string NameEnglish { get; set; }
            public string Description { get; set; }
            public DateTime EventDate { get; set; }
            public int EventPlaceId { get; set; }
        }
}
}
