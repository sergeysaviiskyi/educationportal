namespace EducationPortal.Application.Services
{
    public class UserService : IUserService
    {
        private IUnitOfWork _unitOfWork { get; set; }
        private IMapper _mapper { get; set; }
        private IPasswordService _passwordService { get; set; }
        private IAuthenticationService _authenticationService { get; set; }
        public UserService(IUnitOfWork unitOfWork, IMapper mapper, IPasswordService passwordService, IAuthenticationService authenticationService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _passwordService = passwordService;
            _authenticationService = authenticationService;
        }
        public async Task<Result<User>> GetAsync(int id)
        {
            if (await _unitOfWork.Users.IsExisting(id))
            {
                var user = await _unitOfWork.Users.GetAsync(id);
                return new SuccessResult<User>(_mapper.Map<User>(user));
            }
            var errors = new List<Error>
            {
                new Error("UserdId", "User with such id doesn't exist.")
            };
            return new ErrorResult<User>(errors);
        }
        public async Task<Result<IEnumerable<User>>> GetAllAsync()
        {
            var users = await _unitOfWork.Users.GetAllAsync();
            return new SuccessResult<IEnumerable<User>>(_mapper.Map<List<User>>(users));
        }
        public async Task<Result<User>> GetWithFullInfoAsync(int id)
        {
            if (await _unitOfWork.Users.IsExisting(id))
            {
                var user = await _unitOfWork.Users.GetWithFullInfoAsync(id);
                return new SuccessResult<User>(_mapper.Map<User>(user));
            }
            var errors = new List<Error>
            {
                new Error("UserdId", "User with such id doesn't exist.")
            };
            return new ErrorResult<User>(errors);
        }
        public async Task<Result<IEnumerable<User>>> GetAllWithFullInfoAsync()
        {
            var users = await _unitOfWork.Users.GetAllWithFullInfoAsync();
            return new SuccessResult<IEnumerable<User>>(_mapper.Map<List<User>>(users));
        }
        public async Task<Result<RegisterUserResponse>> CreateAsync(RegisterUserRequest user)
        {
            var binaryPassword = Encoding.UTF8.GetBytes(user.Password);
            var salt = _passwordService.GenerateSalt();
            var hash = _passwordService.HashPassword(binaryPassword, salt);
            var accessToken = _authenticationService.GenerateJWToken(user.Name);
            var refreshToken = _authenticationService.GenerateRefreshToken();
            var refreshTokenModel = new RefreshTokenModel { Token = refreshToken, CreationDate = DateTime.Now, ExpiryDate = DateTime.Now.AddDays(2) };
            var createdUser = new UserModel
            {
                Name = user.Name,
                Email = user.Email,
                Password = hash,
                Salt = salt,
                RefreshToken = refreshTokenModel,
            };
            await _unitOfWork.Users.AddAsync(createdUser);
            await _unitOfWork.CompleteAsync();
            var response = new RegisterUserResponse
            {
                Name = user.Name,
                Email = user.Email,
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
            return new SuccessResult<RegisterUserResponse>(response);
        }
        public async Task<Result> RemoveAsync(int id)
        {
            if (await _unitOfWork.Users.IsExisting(id))
            {
                var userToDelete = await _unitOfWork.Users.GetAsync(id);
                _unitOfWork.Users.Remove(userToDelete);
                await _unitOfWork.CompleteAsync();
                return new SuccessResult();
            }
            else
            {
                var errors = new List<Error>
                {
                new Error("UserId", "User with such id doesn't exist.")
                };
                return new ErrorResult(errors);
            }
        }
    }
}