namespace EducationPortal.Application.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private TimeSpan ExpiryDuration = new TimeSpan(4, 0, 0);
        private IUnitOfWork _unitOfWork { get; set; }
        private IPasswordService _passwordService { get; set; }
        private IEmailSenderService _emailService { get; set; }
        private JWTSettings _jwtSettings { get; set; }

        private readonly Random _random = new Random();
        public AuthenticationService(IUnitOfWork unitOfWork, IPasswordService passwordService, IEmailSenderService emailService, IOptionsSnapshot<JWTSettings> jwtSettings)
        {
            _unitOfWork = unitOfWork;
            _passwordService = passwordService;
            _emailService = emailService;
            _jwtSettings = jwtSettings.Value;
        }
        public async Task<AuthenticationResponse> AuthenticateAsync(AuthenticationRequest request)
        {
            var user = await _unitOfWork.Users.FindWithFullinfoAsync(u => u.Email == request.Email);
            var binaryPassword = Encoding.UTF8.GetBytes(request.Password);
            var salt = user.Salt;
            if (_passwordService.VerifyHashedPassword(binaryPassword, salt, user.Password))
            {
                var accessToken = GenerateJWToken(user.Name);
                var refreshToken = GenerateRefreshToken();
                user.RefreshToken.Token = refreshToken;
                user.RefreshToken.CreationDate = DateTime.Now;
                user.RefreshToken.ExpiryDate = DateTime.Now.AddDays(2);
                await _unitOfWork.CompleteAsync();
                return new AuthenticationResponse { AccessToken = accessToken, RefreshToken = refreshToken };
            }
            else return null;
        }
        public async Task<AuthenticationResponse> RefreshTokenAsync(RefreshTokenRequest request)
        {
            var principal = GetPrincipalFromExpiredToken(request.AccessToken);
            var userName = principal.Identity.Name;
            var user = await _unitOfWork.Users.FindWithFullinfoAsync(u => u.Name == userName);
            if (user is null || user.RefreshToken.Token != request.RefreshToken || user.RefreshToken.ExpiryDate <= DateTime.Now)
                return null;
            var newAccessToken = GenerateJWToken(user.Name);
            var refreshToken = GenerateRefreshToken();
            user.RefreshToken.Token = refreshToken;
            user.RefreshToken.ExpiryDate = DateTime.Now.AddDays(2);
            await _unitOfWork.CompleteAsync();
            return new AuthenticationResponse() { AccessToken = newAccessToken, RefreshToken = refreshToken };
        }
        public Task RevokeTokenAsync()
        {
            throw new NotImplementedException();
        }
        public async Task ForgotPasswordAsync(ForgotPasswordRequest request)
        {
            var user = await _unitOfWork.Users.FindAsync(u => u.Email == request.UserEmail);
            if (user == null) return;
            var code = GenerateVerificationCode(6);

            var message = new EmailData
            {
                To = request.UserEmail,
                Subject = "Reset Password",
                Body = $"Your verification code is: {code}"
            };
            try
            {
                await _emailService.SendEmailAsync(message);
            }
            catch
            {
                return;
            }
            var binaryCode = Encoding.UTF8.GetBytes(code);
            var salt = _passwordService.GenerateSalt();
            var hash = _passwordService.HashPassword(binaryCode, salt);

            var verificarionCode = new VerificationCodeModel
            {
                Salt = salt,
                Code = hash,
                ExpiryDate = DateTime.Now.AddMinutes(10),
            };

            user.VerificationCode = verificarionCode;
            await _unitOfWork.VerificationCode.AddAsync(verificarionCode);
            await _unitOfWork.CompleteAsync();
            return;
        }
        public async Task<bool> ResetPassword(ResetPasswordRequest request)
        {
            var user = await _unitOfWork.Users.FindWithFullinfoAsync(u => u.Email == request.UserEmail);
            var binaryCode = Encoding.UTF8.GetBytes(request.VerificationCode);
            var codeSalt = user.VerificationCode.Salt;
            if (user.VerificationCode != null || user.VerificationCode.ExpiryDate <= DateTime.Now ||
                !_passwordService.VerifyHashedPassword(binaryCode, codeSalt, user.VerificationCode.Code)) return false;
            var binaryPassword = Encoding.UTF8.GetBytes(request.Password);
            var passwordSalt = _passwordService.GenerateSalt();
            var hashedPassword = _passwordService.HashPassword(binaryPassword, passwordSalt);
            user.Salt = passwordSalt;
            user.Password = hashedPassword;
            await _unitOfWork.CompleteAsync();
            return true;
        }
        public string GenerateJWToken(string userName)
        {
            var claims = new List<Claim> { new Claim(ClaimTypes.Name, userName) };

            var takeOptions = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims: claims,
            expires: DateTime.Now.Add(ExpiryDuration),
            signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key)), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(takeOptions);
            return encodedJwt;
        }
        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }
        private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key)),
                ValidateLifetime = false
            };
            //var tokenHandler = new JsonWebTokenHandler();
            //SecurityToken securityToken;
            //var validationResult = tokenHandler.ValidateToken(token, tokenValidationParameters);
            //var identity = validationResult.ClaimsIdentity;
            //securityToken = validationResult.SecurityToken;
            //var jwtSecurityToken = securityToken as JwtSecurityToken;
            //if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            //    throw new SecurityTokenException("Invalid token");
            //return identity;

            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");
            return principal;
        }
        private string GenerateVerificationCode(int length)
        {
            var builder = new StringBuilder();
            for (var i = 0; i < length; i++)
            {
                if (i % 2 == 0)
                {
                    RandomChar(builder);
                    continue;
                }
                RandomNumber(builder);
            }
            return builder.ToString();
        }
        private void RandomNumber(StringBuilder builder, int min = 0, int max = 10)
        {
            builder.Append(_random.Next(min, max));
        }
        private void RandomChar(StringBuilder builder)
        {
            const int lettersOffset = 26;
            var symbol = (char)_random.Next('A', 'A' + lettersOffset);
            builder.Append(symbol);
        }
        //public IEnumerable<Object> Config()
        //{
        //    var jwt = new
        //    {
        //        Issuer = _jwtSettings.Issuer,
        //        Audience = _jwtSettings.Audience,
        //        Key = _jwtSettings.Key,
        //    };
        //    var email = new
        //    {
        //        From = _emailConfig.From,
        //        Host = _emailConfig.Host,
        //        Port = _emailConfig.Port,
        //        UserName = _emailConfig.UserName,
        //        Password = _emailConfig.Password,
        //    };
        //    return new List<Object> { jwt, email };
        //}
    }
}