

using Events.Application.Common.Interfaces.Authentication;
using Events.Contracts.Wrappers;
using Events.Domain.Entities;
using Events.Domain.Enums;
using Events.Infrastructure.UnitOfWork;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Events.Application.TitleSecond.Commands
{
    public class UpdateTitleSecond : IRequest<Response<bool>>
    {
        private readonly UpdateTitleSecondDto _updateTitleDto;
        public UpdateTitleSecond(UpdateTitleSecondDto updateTitleDto)
        {

            _updateTitleDto = updateTitleDto;

        }
        public class UpdateTitleSecondHandler : BaseHandler, IRequestHandler<UpdateTitleSecond, Response<bool>>
        {
            public UpdateTitleSecondHandler(IUnitOfWork iUnitOfWork, IJwtTokenGenerator jwtTokenGenerator, UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor) : base(iUnitOfWork, jwtTokenGenerator, userManager, httpContextAccessor)
            {
            }

            public async Task<Response<bool>> Handle(UpdateTitleSecond request, CancellationToken cancellationToken)
            {
                var obj = await _unitOfWork.TitleSecondRepository.GetById(request._updateTitleDto.Id);
                if(obj is not null)
                obj.UpdateTitleSecond(request._updateTitleDto.Name);
                return await _unitOfWork.CommitAsync();
            }
        }
        public class UpdateTitleSecondDto
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
    }
}
