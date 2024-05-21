

using Events.Application.Common.Interfaces.Authentication;
using Events.Contracts.Wrappers;
using Events.Domain.Entities;
using Events.Domain.Enums;
using Events.Infrastructure.UnitOfWork;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Events.Application.PersonType.Commands
{
    public class CreatePersonType : IRequest<Response<bool>>
    {
        private readonly CreatePersonTypeDto _CreatePersonTypeDto;
        public CreatePersonType(CreatePersonTypeDto CreatePersonTypeDto)
        {

            _CreatePersonTypeDto = CreatePersonTypeDto;

        }
        public class CreatePersonTypeHandler : BaseHandler, IRequestHandler<CreatePersonType, Response<bool>>
        {
            public CreatePersonTypeHandler(IUnitOfWork iUnitOfWork, 
                IJwtTokenGenerator jwtTokenGenerator, 
                UserManager<ApplicationUser> userManager,
                IHttpContextAccessor httpContextAccessor) : base(iUnitOfWork, jwtTokenGenerator, userManager, httpContextAccessor)
            {
            }

            public async Task<Response<bool>> Handle(CreatePersonType request, CancellationToken cancellationToken)
            {
                var obj = new Domain.Entities.PersonType(
                      request._CreatePersonTypeDto.Name,
                      request._CreatePersonTypeDto.Color);
                _unitOfWork.PersonTypeRepository.Insert(obj);

                return await _unitOfWork.CommitAsync();
            }
        }
        public class CreatePersonTypeDto
        {
            public string Name { get; set; }
            public string Color { get; set; }
        }
    }
}
