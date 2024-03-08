namespace EducationPortal.Application.Interfaces.Services
{
    public interface ICourseRepository : IGenericRepository<CourseModel>
    {
        public Task<CourseModel> GetWithFullInfoAsync(int id);
        public Task<ICollection<CourseModel>> GetAllWithFullInfoAsync();
        public Task<CourseModel> FindWithFullInfoAsync(Expression<Func<CourseModel, bool>> predicate);
        public Task<ICollection<CourseModel>> FindManyWithFullInfoAsync(Expression<Func<CourseModel, bool>> predicate);
        public Task<bool> IsNameUniqueAsync(string name);
    }
}
