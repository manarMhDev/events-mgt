

using Events.Application.Common.Interfaces.Authentication;
using Events.Contracts.Authentication;
using Events.Contracts.Wrappers;
using Events.Domain.Entities;
using Events.Infrastructure.UnitOfWork;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Events.Application.Authentication.Commands.Register
{
    public record RegisterCommand : IRequest<Response<AuthenticationResult>>
    {
        private readonly RegisterRequest _registerRequest;
        public RegisterCommand(RegisterRequest registerRequest)
        {
            _registerRequest = registerRequest;
        }
        public class RegisterCommandHandler : BaseHandler ,  IRequestHandler<RegisterCommand, Response<AuthenticationResult>>
        {
            public RegisterCommandHandler(IUnitOfWork iUnitOfWork, IJwtTokenGenerator jwtTokenGenerator, UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor) : base(iUnitOfWork, jwtTokenGenerator, userManager, httpContextAccessor)
            {
            }

            public async Task<Response<AuthenticationResult>> Handle(RegisterCommand request, CancellationToken cancellationToken)
            {
                // check if user already exists
                var user = _unitOfWork.ApplicationUserRepository.SearchFor(x => x.Email == request._registerRequest.Email).FirstOrDefault();
                if (user is not null)
                    throw new Exception("user already exists");

                // create user (generate unique id)
                else
                {
                    user = new ApplicationUser
                    {
                        FirstName = request._registerRequest.FirstName,
                        LastName = request._registerRequest.LastName,
                        UserName = request._registerRequest.FirstName,
                        Email = request._registerRequest.Email,
                        CreatedAt = DateTime.UtcNow
                    };
                    var result = await _userManager.CreateAsync(user, request._registerRequest.Password);
                }

                // create JWT token
                var token = _jwtTokenGenerator.GenerateToken(user);
                return new AuthenticationResult(user, token);
            }
        }
        
    }
}
