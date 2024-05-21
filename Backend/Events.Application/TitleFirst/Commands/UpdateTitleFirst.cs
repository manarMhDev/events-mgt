

using Events.Application.Common.Interfaces.Authentication;
using Events.Contracts.Wrappers;
using Events.Domain.Entities;
using Events.Domain.Enums;
using Events.Infrastructure.UnitOfWork;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Events.Application.TitleFirst.Commands
{
    public class UpdateTitleFirst : IRequest<Response<bool>>
    {
        private readonly UpdateTitleDto _updateTitleDto;
        public UpdateTitleFirst(UpdateTitleDto updateTitleDto)
        {

            _updateTitleDto = updateTitleDto;

        }
        public class UpdateTitleFirstHandler : BaseHandler, IRequestHandler<UpdateTitleFirst, Response<bool>>
        {
            public UpdateTitleFirstHandler(IUnitOfWork iUnitOfWork, IJwtTokenGenerator jwtTokenGenerator, UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor) : base(iUnitOfWork, jwtTokenGenerator, userManager, httpContextAccessor)
            {
            }

            public async Task<Response<bool>> Handle(UpdateTitleFirst request, CancellationToken cancellationToken)
            {
                var obj = await _unitOfWork.TitleFirstRepository.GetById(request._updateTitleDto.Id);
                if(obj is not null)
                obj.UpdateTitleFirst(request._updateTitleDto.Name);
                return await _unitOfWork.CommitAsync();
            }
        }
        public class UpdateTitleDto
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
    }
}
