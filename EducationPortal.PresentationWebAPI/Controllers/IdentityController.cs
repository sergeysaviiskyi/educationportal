using EducationPortal.Application.Interfaces;

namespace EducationPortal.PresentationWebAPI.Controllers
{
    [AllowAnonymous]
    public class IdentityController : BaseApiController
    {
        private IAuthenticationService _authenticationService { get; set; }
        private IEmailSenderService _emailService { get; set; }
        private IMessageProducer _messageProducer { get; set; }
        private IKafkaMessageProducer _kafkaProducer { get; set; }
        public IdentityController(IAuthenticationService authenticationService,
            IEmailSenderService emailService, IMessageProducer messageProducer,
            IKafkaMessageProducer kafkaProducer)
        {
            _authenticationService = authenticationService;
            _emailService = emailService;
            _messageProducer = messageProducer;
            _kafkaProducer = kafkaProducer;
        }


        [HttpPost("login")]
        public async Task<ActionResult> Login(AuthenticationRequest request)
        {
            var response = await _authenticationService.AuthenticateAsync(request);
            return response != null ? Ok(response) : BadRequest("Wrong password");
        }


        [HttpPost("refresh")]
        public async Task<ActionResult> Refresh(RefreshTokenRequest request)
        {
            try
            {
                var response = await _authenticationService.RefreshTokenAsync(request);
                return response != null ? Ok(response) : BadRequest("Invalid client request");
            }
            catch
            {
                return BadRequest("Invalid JWTsecurityToken");
            }
        }


        [HttpPost("forgot-password")]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordRequest request)
        {
            try
            {
                await _authenticationService.ForgotPasswordAsync(request);
            }
            catch
            {
                return BadRequest("Something went wrong.");
            }
            return Ok("The verification code was sent to your email.");
        }


        [HttpPost("reset-password")]
        public async Task<ActionResult> ResetPassword(ResetPasswordRequest request)
        {
            bool result = await _authenticationService.ResetPassword(request);
            return result ? Ok() : BadRequest("Something went wrong");
        }

        [HttpGet("rabbit-test")]
        public async Task<ActionResult> RabbitTest(string message)
        {
            _messageProducer.PublishMassage(message);
            return Ok("Message sent to Rabbit");
        }

        [HttpGet("kafka-test")]
        public async Task<ActionResult> KafkaTest(string message)
        {
            await _kafkaProducer.PublishMassageAsync(message);
            return Ok("Message sent to Kafka");
        }
    }
}