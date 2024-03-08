namespace EducationPortal.Application.Interfaces.Services
{
    public interface ITokenService
    {
        string BuildToken(User user);
        public string GenerateRefreshToken();
        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token);

    }
}
