namespace EducationPortal.Application.Interfaces.Services
{
    public interface IEmailSenderService
    {
        public Task SendEmailAsync(EmailData message);
    }
}
