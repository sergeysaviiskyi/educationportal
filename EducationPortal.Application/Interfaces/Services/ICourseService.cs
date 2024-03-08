namespace EducationPortal.Application.Interfaces.Services
{
    public interface ICourseService
    {
        public Task<Result<Course>> GetAsync(int id);
        public Task<Result<IEnumerable<Course>>> GetAllAsync();
        public Task<Result<Course>> GetWithFullInfoAsync(int id);
        public Task<Result<IEnumerable<Course>>> GetAllWithFullInfoAsync();
        public Task<Result<Course>> CreateAsync(string name, string description);
        public Task<Result> StartNewCourseAsync(int userId, int courseId);
        public Task<Result> AddMaterialAsync(int courseId, int materialId);
        public Task<Result> AddSkillAsync(int courseId, int skillId);
        public Task<Result> LearnLessonAsync(int userId, int courseId);
        public Task<Result> RemoveAsync(int id);
    }
}