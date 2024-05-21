

using Events.Application.Common.Interfaces.Authentication;
using Events.Contracts.Wrappers;
using Events.Domain.Entities;
using Events.Infrastructure.UnitOfWork;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Events.Application.PersonType.Commands
{
    public class DeletePersonType : IRequest<Response<bool>>
    {
        private readonly int _id;
        public DeletePersonType(int id)
        {

            _id = id;

        }
        public class DeletePersonTypeHandler : BaseHandler, IRequestHandler<DeletePersonType, Response<bool>>
        {
            public DeletePersonTypeHandler(IUnitOfWork iUnitOfWork, IJwtTokenGenerator jwtTokenGenerator, UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor) : base(iUnitOfWork, jwtTokenGenerator, userManager, httpContextAccessor)
            {
            }

            public async Task<Response<bool>> Handle(DeletePersonType request, CancellationToken cancellationToken)
            {
                var obj = await _unitOfWork.PersonTypeRepository.GetById(request._id);
                if(obj is not null)
                {
                    _unitOfWork.PersonTypeRepository.Delete(obj);
                    return await _unitOfWork.CommitAsync();
                }
                return new Response<bool>(false);
            }
        }
    }
}
