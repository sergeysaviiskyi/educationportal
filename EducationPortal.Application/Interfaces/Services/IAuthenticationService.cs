namespace EducationPortal.Application.Interfaces.Services
{
    public interface IAuthenticationService
    {
        public Task<AuthenticationResponse> AuthenticateAsync(AuthenticationRequest request);
        public Task<AuthenticationResponse> RefreshTokenAsync(RefreshTokenRequest request);
        public Task RevokeTokenAsync();
        public Task ForgotPasswordAsync(ForgotPasswordRequest request);
        public Task<bool> ResetPassword(ResetPasswordRequest request);
        public string GenerateJWToken(string userName);
        public string GenerateRefreshToken();
    }
}