

using Events.Application.Common.Interfaces.Authentication;
using Events.Contracts.Wrappers;
using Events.Domain.Entities;
using Events.Infrastructure.UnitOfWork;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using static Events.Application.PersonType.Queries.GetPersonType;
using static Events.Application.TitleSecond.Queries.GetTitleSecond;

namespace Events.Application.EventPlaces.Queries.GetPlaces
{
    public class GetAllPersonTypes : IRequest<Response<List<GetPersonTypeDto>>>
    {
        public GetAllPersonTypes()
        {
        }
        public class GetAllPersonTypesHandler : BaseHandler, IRequestHandler<GetAllPersonTypes, Response<List<GetPersonTypeDto>>>
        {
            public GetAllPersonTypesHandler(IUnitOfWork iUnitOfWork, IJwtTokenGenerator jwtTokenGenerator,
                UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor) : base(iUnitOfWork, jwtTokenGenerator, userManager, httpContextAccessor)
            {
            }

            public async Task<Response<List<GetPersonTypeDto>>> Handle(GetAllPersonTypes request, CancellationToken cancellationToken)
            {
                var result = await _unitOfWork.PersonTypeRepository.SearchFor().Select(item =>
                 new GetPersonTypeDto
                 {
                     Id = item.Id,
                      Name = item.Name,
                        Color = item.Color
                       
                 }).ToListAsync();
                return new Response<List<GetPersonTypeDto>>(result,true);
            }
        }
     

    }
}
