

using Events.Application.Common.Interfaces.Authentication;
using Events.Contracts.Wrappers;
using Events.Domain.Entities;
using Events.Infrastructure.UnitOfWork;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using static Events.Application.TitleSecond.Queries.GetTitleSecond;

namespace Events.Application.EventPlaces.Queries.GetPlaces
{
    public class GetAllTitles2 : IRequest<Response<List<GetTitleSecondDto>>>
    {
        public GetAllTitles2()
        {
        }
        public class GetAllTitles2Handler : BaseHandler, IRequestHandler<GetAllTitles2, Response<List<GetTitleSecondDto>>>
        {
            public GetAllTitles2Handler(IUnitOfWork iUnitOfWork, IJwtTokenGenerator jwtTokenGenerator,
                UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor) : base(iUnitOfWork, jwtTokenGenerator, userManager, httpContextAccessor)
            {
            }

            public async Task<Response<List<GetTitleSecondDto>>> Handle(GetAllTitles2 request, CancellationToken cancellationToken)
            {
                var result = await _unitOfWork.TitleSecondRepository.SearchFor().Select(item =>
                 new GetTitleSecondDto
                 {
                     Id = item.Id,
                      Name = item.Name
                       
                 }).ToListAsync();
                return new Response<List<GetTitleSecondDto>>(result,true);
            }
        }
     

    }
}
