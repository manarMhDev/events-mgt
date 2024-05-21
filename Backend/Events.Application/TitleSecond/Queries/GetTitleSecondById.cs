

using Events.Application.Common.Interfaces.Authentication;
using Events.Contracts.Wrappers;
using Events.Domain.Entities;
using Events.Domain.Enums;
using Events.Infrastructure.UnitOfWork;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using static Events.Application.TitleSecond.Queries.GetTitleSecondById;

namespace Events.Application.TitleSecond.Queries
{
    public class GetTitleSecondById : IRequest<Response<TitleSecondDto>>
    {
        private readonly int _id;
        public GetTitleSecondById(int id)
        {

            _id = id;

        }
        public class GetTitleSecondByIdHandler : BaseHandler, IRequestHandler<GetTitleSecondById, Response<TitleSecondDto>>
        {
            public GetTitleSecondByIdHandler(IUnitOfWork iUnitOfWork, IJwtTokenGenerator jwtTokenGenerator, UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor) : base(iUnitOfWork, jwtTokenGenerator, userManager, httpContextAccessor)
            {
            }

            public async Task<Response<TitleSecondDto>> Handle(GetTitleSecondById request, CancellationToken cancellationToken)
            {
                var obj = await _unitOfWork.TitleSecondRepository.GetById(request._id);
                var model = new TitleSecondDto();
                model.Id = obj.Id;
                model.Name = obj.Name;
                
                return new Response<TitleSecondDto>(model);
            }
        }
        public class TitleSecondDto
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
    }
}
