using Microsoft.Extensions.Options;

namespace EducationPortal.Application.Services
{
    public class EmailSenderService : IEmailSenderService
    {
        private readonly EmailConfiguration _configuration;
        public EmailSenderService(IOptionsSnapshot<EmailConfiguration> configuration)
        {
            _configuration = configuration.Value;
        }
        public async Task SendEmailAsync(EmailData message)
        {
            var emailMessage = CreateEmailMessage(message);
            await SendAsync(emailMessage);
        }
        private MimeMessage CreateEmailMessage(EmailData message)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("Education Portal", _configuration.From));
            emailMessage.To.Add(MailboxAddress.Parse(message.To));
            emailMessage.Subject = message.Subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Text) { Text = message.Body };
            return emailMessage;
        }
        private async Task SendAsync(MimeMessage emailMessage)
        {
            using (var client = new SmtpClient())
            {
                try
                {
                    await client.ConnectAsync(_configuration.Host, _configuration.Port, SecureSocketOptions.StartTls);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    await client.AuthenticateAsync(_configuration.UserName, _configuration.Password);
                    await client.SendAsync(emailMessage);
                }
                catch
                {
                    throw new NotImplementedException("Mail sending process has failed.");
                }
                finally
                {
                    await client.DisconnectAsync(true);
                    client.Dispose();
                }
            }
        }
    }
}
