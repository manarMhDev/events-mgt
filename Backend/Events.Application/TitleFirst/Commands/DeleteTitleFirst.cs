

using Events.Application.Common.Interfaces.Authentication;
using Events.Contracts.Wrappers;
using Events.Domain.Entities;
using Events.Infrastructure.UnitOfWork;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Events.Application.TitleFirst.Commands
{
    public class DeleteTitleFirst : IRequest<Response<bool>>
    {
        private readonly int _id;
        public DeleteTitleFirst(int id)
        {

            _id = id;

        }
        public class DeleteTitleFirstHandler : BaseHandler, IRequestHandler<DeleteTitleFirst, Response<bool>>
        {
            public DeleteTitleFirstHandler(IUnitOfWork iUnitOfWork, IJwtTokenGenerator jwtTokenGenerator, UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor) : base(iUnitOfWork, jwtTokenGenerator, userManager, httpContextAccessor)
            {
            }

            public async Task<Response<bool>> Handle(DeleteTitleFirst request, CancellationToken cancellationToken)
            {
                var obj = await _unitOfWork.TitleFirstRepository.GetById(request._id);
                if(obj is not null)
                {
                    _unitOfWork.TitleFirstRepository.Delete(obj);
                    return await _unitOfWork.CommitAsync();
                }
                return new Response<bool>(false);
            }
        }
    }
}
