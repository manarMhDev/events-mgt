

using Events.Application.Common.Interfaces.Authentication;
using Events.Contracts.Wrappers;
using Events.Domain.Entities;
using Events.Infrastructure.UnitOfWork;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using static Events.Application.EventPlaces.Queries.GetPlaces.GetEventPlaces;
using static Events.Application.TitleFirst.Queries.GetTitleFirst;

namespace Events.Application.EventPlaces.Queries.GetPlaces
{
    public class GetAllTitles1 : IRequest<Response<List<GetTitleFirstDto>>>
    {
        public GetAllTitles1()
        {
        }
        public class GetAllTitles1Handler : BaseHandler, IRequestHandler<GetAllTitles1, Response<List<GetTitleFirstDto>>>
        {
            public GetAllTitles1Handler(IUnitOfWork iUnitOfWork, IJwtTokenGenerator jwtTokenGenerator,
                UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor) : base(iUnitOfWork, jwtTokenGenerator, userManager, httpContextAccessor)
            {
            }

            public async Task<Response<List<GetTitleFirstDto>>> Handle(GetAllTitles1 request, CancellationToken cancellationToken)
            {
                var result = await _unitOfWork.TitleFirstRepository.SearchFor().Select(item =>
                 new GetTitleFirstDto
                 {
                     Id = item.Id,
                      Name = item.Name
                       
                 }).ToListAsync();
                return new Response<List<GetTitleFirstDto>>(result,true);
            }
        }
     

    }
}
