

using Events.Application.Common.Interfaces.Authentication;
using Events.Contracts.Wrappers;
using Events.Domain.Entities;
using Events.Infrastructure.UnitOfWork;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Events.Application.PersonType.Commands
{
    public class DeleteInvitation : IRequest<Response<bool>>
    {
        private readonly int _id;
        public DeleteInvitation(int id)
        {

            _id = id;

        }
        public class DeleteInvitationHandler : BaseHandler, IRequestHandler<DeleteInvitation, Response<bool>>
        {
            public DeleteInvitationHandler(IUnitOfWork iUnitOfWork, IJwtTokenGenerator jwtTokenGenerator, UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor) : base(iUnitOfWork, jwtTokenGenerator, userManager, httpContextAccessor)
            {
            }

            public async Task<Response<bool>> Handle(DeleteInvitation request, CancellationToken cancellationToken)
            {
                var obj = await _unitOfWork.InvitationRepository.GetById(request._id);
                if(obj is not null)
                {
                    _unitOfWork.InvitationRepository.Delete(obj);
                    return await _unitOfWork.CommitAsync();
                }
                return new Response<bool>(false);
            }
        }
    }
}
