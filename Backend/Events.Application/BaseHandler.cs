
using Events.Application.Common.Interfaces.Authentication;
using Events.Application.Helpers;
using Events.Domain.Entities;
using Events.Infrastructure.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Events.Application
{
    public abstract class BaseHandler
    {
        protected IJwtTokenGenerator _jwtTokenGenerator { get; set; }
        protected  IUnitOfWork _unitOfWork { get; set; }
        protected UserManager<ApplicationUser> _userManager { get; set; }
        private string userId;
        protected string _UserId { get => GetUserId(); }
        protected IHttpContextAccessor _HttpContextAccessor { get; }
        public BaseHandler(IUnitOfWork iUnitOfWork, 
            IJwtTokenGenerator jwtTokenGenerator,
            UserManager<ApplicationUser> userManager,
            IHttpContextAccessor httpContextAccessor)
        {
             _unitOfWork = iUnitOfWork;
             _jwtTokenGenerator = jwtTokenGenerator;
            _HttpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }
        private string GetUserId()
        {
            if (String.IsNullOrEmpty(userId))
            {
                userId = _HttpContextAccessor.HttpContext.User.GetLoggedInUserId();
            }
            return userId;
        }
    }
}
