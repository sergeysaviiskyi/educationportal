namespace EducationPortal.Application.DTOs.Authentication
{
    public class ResetPasswordRequest
    {
        public string UserEmail{ get; set; }

        public string VerificationCode { get; set; }

        public string Password { get; set; }

        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}