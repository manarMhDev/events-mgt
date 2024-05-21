

using Events.Application.Common.Interfaces.Authentication;
using Events.Application.Common.Interfaces.Services;
using Events.Domain.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace Events.Infrastructure.Authentication
{
    public class JwtTokenGenerator : IJwtTokenGenerator
    {
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly JwtSettings _jwtSettings;
        public JwtTokenGenerator(IDateTimeProvider dateTimeProvider, IOptions<JwtSettings> jwtOptions)
        {
            _dateTimeProvider = dateTimeProvider;
            _jwtSettings = jwtOptions.Value;
        }
        public string GenerateToken(ApplicationUser user)
        {
            var claims = new List<Claim>
             {
                 new Claim(ClaimTypes.Email, user.Email??""),
                  new Claim("UserId", user.Id),
                   new Claim("UserName", user.UserName??""),
                  //new Claim("Role", user.UserRoles.FirstOrDefault()?.Role?.Name ??"")
             };
            var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);
            SigningCredentials signingCred = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature);
            var securityToken = new JwtSecurityToken(
                claims: claims,
                expires: _dateTimeProvider.UtcNow.AddMinutes(_jwtSettings.ExpiryMinutes),
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                signingCredentials: signingCred);
            string token = new JwtSecurityTokenHandler().WriteToken(securityToken);
            return token;

        }
    }
}
