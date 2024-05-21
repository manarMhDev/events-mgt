

using Events.Application.Common.Interfaces.Authentication;
using Events.Contracts.Services.Interfaces;
using Events.Contracts.Wrappers;
using Events.Domain.Entities;
using Events.Domain.Enums;
using Events.Infrastructure.UnitOfWork;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Events.Application.TitleSecond.Commands
{
    public class CreateTitleSecond : IRequest<Response<bool>>
    {
        private readonly CreateSecondTitleDto _CreateSecondTitleDto;
        public CreateTitleSecond(CreateSecondTitleDto CreateSecondTitleDto)
        {

            _CreateSecondTitleDto = CreateSecondTitleDto;

        }
        public class CreateTitleSecondHandler : BaseHandler, IRequestHandler<CreateTitleSecond, Response<bool>>
        {
            public CreateTitleSecondHandler(IUnitOfWork iUnitOfWork, 
                IJwtTokenGenerator jwtTokenGenerator, 
                UserManager<ApplicationUser> userManager,
                IHttpContextAccessor httpContextAccessor) : base(iUnitOfWork, jwtTokenGenerator, userManager, httpContextAccessor)
            {
            }

            public async Task<Response<bool>> Handle(CreateTitleSecond request, CancellationToken cancellationToken)
            {
                var obj = new Domain.Entities.TitleSecond(
                      request._CreateSecondTitleDto.Name);
                _unitOfWork.TitleSecondRepository.Insert(obj);

                return await _unitOfWork.CommitAsync();
            }
        }
        public class CreateSecondTitleDto
        {
            public string Name { get; set; }
        }
    }
}
