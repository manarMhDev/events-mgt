

using Events.Application.Common.Interfaces.Authentication;
using Events.Contracts.Wrappers;
using Events.Domain.Entities;
using Events.Domain.Enums;
using Events.Infrastructure.UnitOfWork;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using static Events.Application.SeatsTypes.Queries.GetAllSeatTypes;

namespace Events.Application.SeatsTypes.Queries
{
    public class GetAllSeatTypes : IRequest<Response<List<AllSeatsTypeDto>>>
    {
        public GetAllSeatTypes()
        {
        }
        public class GetAllSeatTypesHandler : BaseHandler, IRequestHandler<GetAllSeatTypes, Response<List<AllSeatsTypeDto>>>
        {
            public GetAllSeatTypesHandler(IUnitOfWork iUnitOfWork, IJwtTokenGenerator jwtTokenGenerator,
                UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor) : base(iUnitOfWork, jwtTokenGenerator, userManager, httpContextAccessor)
            {
            }

            public async Task<Response<List<AllSeatsTypeDto>>> Handle(GetAllSeatTypes request, CancellationToken cancellationToken)
            {
                var result = await _unitOfWork.SeatsTypeRepository.SearchFor().Select(item =>
                 new AllSeatsTypeDto
                 {
                     Id = item.Id,
                     Name = item.Name,
                     Color = item.Color,
                     ColorText = item.ColorText,
                     ImagePath = item.ImagePath
                 }).ToListAsync();
                return new Response<List<AllSeatsTypeDto>>(result, true);
            }
        }
        public class AllSeatsTypeDto
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Color { get; set; }
            public string ColorText { get; set; }
            public string ImagePath { get; set; }

        }

    }
}
