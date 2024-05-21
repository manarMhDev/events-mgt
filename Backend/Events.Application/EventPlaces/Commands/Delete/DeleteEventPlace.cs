
using Events.Application.Common.Interfaces.Authentication;
using Events.Application.PersonType.Commands;
using Events.Contracts.Wrappers;
using Events.Domain.Entities;
using Events.Infrastructure.UnitOfWork;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Events.Application.EventPlaces.Commands.Delete
{
    public class DeleteEventPlace : IRequest<Response<bool>>
    {
        private readonly int _id;
        public DeleteEventPlace(int id)
        {

            _id = id;

        }
        public class DeleteEventPlaceHandler : BaseHandler, IRequestHandler<DeleteEventPlace, Response<bool>>
        {
            public DeleteEventPlaceHandler(IUnitOfWork iUnitOfWork, IJwtTokenGenerator jwtTokenGenerator, UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor) : base(iUnitOfWork, jwtTokenGenerator, userManager, httpContextAccessor)
            {
            }

            public async Task<Response<bool>> Handle(DeleteEventPlace request, CancellationToken cancellationToken)
            {
                var obj = await _unitOfWork.EventPlaceRepository.GetById(request._id);
                if (obj is not null)
                {
                    _unitOfWork.EventPlaceRepository.Delete(obj);
                    return await _unitOfWork.CommitAsync();
                }
                return new Response<bool>(false);
            }
        }
    }
}
