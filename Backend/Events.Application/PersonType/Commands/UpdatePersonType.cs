

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
    public class UpdatePersonType : IRequest<Response<bool>>
    {
        private readonly UpdatePersonTypeDto _updatePersonTYpeDto;
        public UpdatePersonType(UpdatePersonTypeDto updatePersonTYpeDto)
        {

            _updatePersonTYpeDto = updatePersonTYpeDto;

        }
        public class UpdatePersonTypeHandler : BaseHandler, IRequestHandler<UpdatePersonType, Response<bool>>
        {
            public UpdatePersonTypeHandler(IUnitOfWork iUnitOfWork, IJwtTokenGenerator jwtTokenGenerator, UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor) : base(iUnitOfWork, jwtTokenGenerator, userManager, httpContextAccessor)
            {
            }

            public async Task<Response<bool>> Handle(UpdatePersonType request, CancellationToken cancellationToken)
            {
                var obj = await _unitOfWork.PersonTypeRepository.GetById(request._updatePersonTYpeDto.Id);
                if(obj is not null)
                obj.UpdatePersonType(request._updatePersonTYpeDto.Name,request._updatePersonTYpeDto.Color);
                return await _unitOfWork.CommitAsync();
            }
        }
        public class UpdatePersonTypeDto
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Color { get; set; }
        }
    }
}
