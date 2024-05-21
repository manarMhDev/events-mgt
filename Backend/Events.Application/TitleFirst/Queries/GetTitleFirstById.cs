

using Events.Application.Common.Interfaces.Authentication;
using Events.Contracts.Wrappers;
using Events.Domain.Entities;
using Events.Domain.Enums;
using Events.Infrastructure.UnitOfWork;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using static Events.Application.TitleFirst.Queries.GetTitleFirstById;

namespace Events.Application.TitleFirst.Queries
{
    public class GetTitleFirstById : IRequest<Response<TitleFirstDto>>
    {
        private readonly int _id;
        public GetTitleFirstById(int id)
        {

            _id = id;

        }
        public class GetTitleFirstByIdHandler : BaseHandler, IRequestHandler<GetTitleFirstById, Response<TitleFirstDto>>
        {
            public GetTitleFirstByIdHandler(IUnitOfWork iUnitOfWork, IJwtTokenGenerator jwtTokenGenerator, UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor) : base(iUnitOfWork, jwtTokenGenerator, userManager, httpContextAccessor)
            {
            }

            public async Task<Response<TitleFirstDto>> Handle(GetTitleFirstById request, CancellationToken cancellationToken)
            {
                var obj = await _unitOfWork.TitleFirstRepository.GetById(request._id);
                var model = new TitleFirstDto();
                model.Id = obj.Id;
                model.Name = obj.Name;
                
                return new Response<TitleFirstDto>(model);
            }
        }
        public class TitleFirstDto
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
    }
}
