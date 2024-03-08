namespace EducationPortal.Application.DTOs.User
{
    public class RegisterUserResponse
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
