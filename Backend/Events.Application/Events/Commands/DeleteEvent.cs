

using Events.Application.Common.Interfaces.Authentication;
using Events.Application.EventPlaces.Commands.Delete;
using Events.Contracts.Wrappers;
using Events.Domain.Entities;
using Events.Infrastructure.UnitOfWork;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Events.Application.Events.Commands
{
    public class DeleteEvent : IRequest<Response<bool>>
    {
        private readonly int _id;
        public DeleteEvent(int id)
        {

            _id = id;

        }
        public class DeleteEventHandler : BaseHandler, IRequestHandler<DeleteEvent, Response<bool>>
        {
            public DeleteEventHandler(IUnitOfWork iUnitOfWork, IJwtTokenGenerator jwtTokenGenerator, UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor) : base(iUnitOfWork, jwtTokenGenerator, userManager, httpContextAccessor)
            {
            }

            public async Task<Response<bool>> Handle(DeleteEvent request, CancellationToken cancellationToken)
            {
                var obj = await _unitOfWork.EventRepository.GetById(request._id);
                if (obj is not null)
                {
                    _unitOfWork.EventRepository.Delete(obj);
                    return await _unitOfWork.CommitAsync();
                }
                return new Response<bool>(false);
            }
        }
    }
}
