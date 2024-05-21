

using Events.Application.Common.Interfaces.Authentication;
using Events.Contracts.Dtos;
using Events.Contracts.Extensions;
using Events.Contracts.Wrappers;
using Events.Domain.Entities;
using Events.Domain.Enums;
using Events.Infrastructure.UnitOfWork;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using static Events.Application.SeatsTypes.Queries.GetSeatsTypes;

namespace Events.Application.SeatsTypes.Queries
{
    public class GetSeatsTypes : IRequest<Response<PagedResult<GetSeatsTypeDto>>>
    {
        private readonly int _page;
        private readonly int _size;
        public GetSeatsTypes(int page, int size)
        {
            _page = page;
            _size = size;
        }
        public class GetSeatsTypesHandler : BaseHandler, IRequestHandler<GetSeatsTypes, Response<PagedResult<GetSeatsTypeDto>>>
        {
            public GetSeatsTypesHandler(IUnitOfWork iUnitOfWork, IJwtTokenGenerator jwtTokenGenerator, UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor) : base(iUnitOfWork, jwtTokenGenerator, userManager, httpContextAccessor)
            {
            }

            public async Task<Response<PagedResult<GetSeatsTypeDto>>> Handle(GetSeatsTypes request, CancellationToken cancellationToken)
            {
                var result = _unitOfWork.SeatsTypeRepository.SearchFor().Select(item =>
             new GetSeatsTypeDto
             {
                 Id = item.Id,
                 Name = item.Name,
                 Color = item.Color,
                 ColorText = item.ColorText,
                 ImagePath = item.ImagePath
             }) .ToQueryResult(request._page, request._size);
                return new Response<PagedResult<GetSeatsTypeDto>>(result, true);
            }
        }
        public class GetSeatsTypeDto
        {
            public int Id { get;  set; }
            public string Name { get;  set; }
            public string Color { get;  set; }
            public string ColorText { get;  set; }
            public string ImagePath { get;  set; }
        }
    }
}
