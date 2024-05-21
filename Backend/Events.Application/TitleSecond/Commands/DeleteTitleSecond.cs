

using Events.Application.Common.Interfaces.Authentication;
using Events.Contracts.Wrappers;
using Events.Domain.Entities;
using Events.Infrastructure.UnitOfWork;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Events.Application.TitleSecond.Commands
{
    public class DeleteTitleSecond : IRequest<Response<bool>>
    {
        private readonly int _id;
        public DeleteTitleSecond(int id)
        {

            _id = id;

        }
        public class DeleteTitleSecondHandler : BaseHandler, IRequestHandler<DeleteTitleSecond, Response<bool>>
        {
            public DeleteTitleSecondHandler(IUnitOfWork iUnitOfWork, IJwtTokenGenerator jwtTokenGenerator, UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor) : base(iUnitOfWork, jwtTokenGenerator, userManager, httpContextAccessor)
            {
            }

            public async Task<Response<bool>> Handle(DeleteTitleSecond request, CancellationToken cancellationToken)
            {
                var obj = await _unitOfWork.TitleSecondRepository.GetById(request._id);
                if(obj is not null)
                {
                    _unitOfWork.TitleSecondRepository.Delete(obj);
                    return await _unitOfWork.CommitAsync();
                }
                return new Response<bool>(false);
            }
        }
    }
}
