

using Events.Domain.Entities;

namespace Events.Contracts.Authentication
{
    public record AuthenticationResult(ApplicationUser User, string Token);
}
