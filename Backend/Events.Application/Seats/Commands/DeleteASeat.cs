

using Events.Application.Common.Interfaces.Authentication;
using Events.Application.SeatsTypes.Commands;
using Events.Contracts.Wrappers;
using Events.Domain.Entities;
using Events.Infrastructure.UnitOfWork;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Events.Application.Seats.Commands
{
    public class DeleteASeat : IRequest<Response<bool>>
    {
        private readonly int _id;
        public DeleteASeat(int id)
        {

            _id = id;

        }
        public class DeleteASeatHandler : BaseHandler, IRequestHandler<DeleteASeat, Response<bool>>
        {
            public DeleteASeatHandler(IUnitOfWork iUnitOfWork, IJwtTokenGenerator jwtTokenGenerator, UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor) : base(iUnitOfWork, jwtTokenGenerator, userManager, httpContextAccessor)
            {
            }

            public async Task<Response<bool>> Handle(DeleteASeat request, CancellationToken cancellationToken)
            {
                var obj = await _unitOfWork.PlaceSeatRepository.GetById(request._id);
                if (obj is not null)
                {
                    _unitOfWork.PlaceSeatRepository.Delete(obj);
                    return await _unitOfWork.CommitAsync();
                }
                return new Response<bool>(false);
            }
        }
    }
}
