namespace EducationPortal.Application.Interfaces.Services
{
    public interface IUserService
    {
        public Task<Result<User>> GetAsync(int id);
        public Task<Result<IEnumerable<User>>> GetAllAsync();
        public Task<Result<User>> GetWithFullInfoAsync(int id);
        public Task<Result<IEnumerable<User>>> GetAllWithFullInfoAsync();
        public Task<Result<RegisterUserResponse>> CreateAsync(RegisterUserRequest user);
        public Task<Result> RemoveAsync(int id);
    }
}