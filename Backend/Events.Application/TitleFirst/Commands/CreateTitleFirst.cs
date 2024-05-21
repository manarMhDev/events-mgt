

using Events.Application.Common.Interfaces.Authentication;
using Events.Contracts.Services.Interfaces;
using Events.Contracts.Wrappers;
using Events.Domain.Entities;
using Events.Domain.Enums;
using Events.Infrastructure.UnitOfWork;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Events.Application.TitleFirst.Commands
{
    public class CreateTitleFirst : IRequest<Response<bool>>
    {
        private readonly CreateFirstTitleDto _createFirstTitleDto;
        public CreateTitleFirst(CreateFirstTitleDto createFirstTitleDto)
        {

            _createFirstTitleDto = createFirstTitleDto;

        }
        public class CreateTitleFirstHandler : BaseHandler, IRequestHandler<CreateTitleFirst, Response<bool>>
        {
            public CreateTitleFirstHandler(IUnitOfWork iUnitOfWork, 
                IJwtTokenGenerator jwtTokenGenerator, 
                UserManager<ApplicationUser> userManager,
                IHttpContextAccessor httpContextAccessor) : base(iUnitOfWork, jwtTokenGenerator, userManager, httpContextAccessor)
            {
            }

            public async Task<Response<bool>> Handle(CreateTitleFirst request, CancellationToken cancellationToken)
            {
                var obj = new  Domain.Entities.TitleFirst(
                      request._createFirstTitleDto.Name);
                _unitOfWork.TitleFirstRepository.Insert(obj);

                return await _unitOfWork.CommitAsync();
            }
        }
        public class CreateFirstTitleDto
        {
            public string Name { get; set; }
        }
    }
}
