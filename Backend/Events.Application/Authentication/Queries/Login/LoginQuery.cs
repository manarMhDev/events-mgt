
using Events.Application.Common.Interfaces.Authentication;
using Events.Contracts.Authentication;
using Events.Contracts.Wrappers;
using Events.Domain.Entities;
using Events.Infrastructure.UnitOfWork;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Events.Application.Authentication.Queries.Login
{
    public class LoginQuery : IRequest<Response<AuthenticationResult>>
    {
        private readonly LoginRequest _loginRequest;
        public LoginQuery(LoginRequest loginRequest)
        {
            _loginRequest = loginRequest;
        }
        public class LoginQueryHandler : BaseHandler, IRequestHandler<LoginQuery, Response<AuthenticationResult>>
        {
            public LoginQueryHandler(IUnitOfWork iUnitOfWork, IJwtTokenGenerator jwtTokenGenerator, UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor) : base(iUnitOfWork, jwtTokenGenerator, userManager, httpContextAccessor)
            {
            }

            public async Task<Response<AuthenticationResult>> Handle(LoginQuery request, CancellationToken cancellationToken)
            {
                // validate user exists
                var user = _unitOfWork.ApplicationUserRepository.SearchFor(x => x.Email == request._loginRequest.Email).FirstOrDefault();
                if (user is null)
                    throw new Exception("user donesn't exists");

                // validate password is correct
                if (await _userManager.CheckPasswordAsync(user,request._loginRequest.Password))
                {
                    //generate token
                    var token = _jwtTokenGenerator.GenerateToken(user);



                    // return token 
                    return new AuthenticationResult(user, token);
                }

                return new Response<AuthenticationResult>(false);

            }
        }
    }
}
