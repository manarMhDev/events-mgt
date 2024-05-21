
using Events.Application.Authentication.Commands.Register;
using Events.Application.Authentication.Queries.Login;
using Events.Contracts.Authentication;
using Events.Contracts.Wrappers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Events.Api.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthenticationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("register")]
        public async Task<Response<AuthenticationResponse>> Register(RegisterRequest request)
        {
            var authResult = await _mediator.Send(new RegisterCommand(request));
            var response = new AuthenticationResponse(
                authResult.Data.User.Id,
                authResult.Data.User.UserName,
                authResult.Data.User.Email,
                authResult.Data.Token);
            return new Response<AuthenticationResponse>(response);
        }   
        [HttpPost("login")]
        public async Task<Response<AuthenticationResponse>> Login(LoginRequest request)
        {
            var authResult = await _mediator.Send(new LoginQuery(request));
            var response = new AuthenticationResponse(
                authResult.Data.User.Id, 
                authResult.Data.User.UserName,
                authResult.Data.User.Email, 
                authResult.Data.Token);
            return new Response<AuthenticationResponse>(response);
        }

    }
}