namespace EducationPortal.Application.Interfaces.Repositories
{
    public interface IUserRepository : IGenericRepository<UserModel>
    {
        public Task<UserModel> GetWithFullInfoAsync(int id);
        public Task<ICollection<UserModel>> GetAllWithFullInfoAsync();
        public Task<UserModel> FindWithFullinfoAsync(Expression<Func<UserModel, bool>> predicate);
        public Task<ICollection<UserModel>> FindManyWithFullinfoAsync(Expression<Func<UserModel, bool>> predicate);
        public Task<bool> IsExistingEmail(string email);
        public Task<bool> CanLearnCourse(int userId, int corseId);
    }
}
