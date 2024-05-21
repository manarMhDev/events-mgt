

namespace Events.Contracts.Authentication
{
    public record AuthenticationResponse(
    string Id,
    string username,
    string Email,
    string Token
    );
}
