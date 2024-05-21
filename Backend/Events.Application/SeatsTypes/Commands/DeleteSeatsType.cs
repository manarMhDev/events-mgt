

using Events.Application.Common.Interfaces.Authentication;
using Events.Contracts.Wrappers;
using Events.Domain.Entities;
using Events.Infrastructure.UnitOfWork;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Events.Application.SeatsTypes.Commands
{
    public class DeleteSeatTypes : IRequest<Response<bool>>
    {
        private readonly int _id;
        public DeleteSeatTypes(int id)
        {

            _id = id;

        }
        public class DeleteSeatTypesHandler : BaseHandler, IRequestHandler<DeleteSeatTypes, Response<bool>>
        {
            public DeleteSeatTypesHandler(IUnitOfWork iUnitOfWork, IJwtTokenGenerator jwtTokenGenerator, UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor) : base(iUnitOfWork, jwtTokenGenerator, userManager, httpContextAccessor)
            {
            }

            public async Task<Response<bool>> Handle(DeleteSeatTypes request, CancellationToken cancellationToken)
            {
                var obj = await _unitOfWork.SeatsTypeRepository.GetById(request._id);
                if(obj is not null)
                {
                    _unitOfWork.SeatsTypeRepository.Delete(obj);
                    return await _unitOfWork.CommitAsync();
                }
                return new Response<bool>(false);
            }
        }
    }
}
