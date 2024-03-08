namespace EducationPortal.Application.Interfaces.Services
{
    public interface IUnitOfWork : IDisposable
    {
        public IUserRepository Users { get; }
        public ICourseRepository Courses { get; }
        public IMaterialRepository Materials { get; }
        public ISkillRepository Skills { get; }
        public IUserCourseRepository UserCourses { get; }
        public IUserSkillRepository UsersSkills { get; }
        public ICourseMaterialRepository CourseMaterials { get; }
        public ICourseSkillRepository CourseSkills { get; }
        public IVerificationCodeRepository VerificationCode { get; }
        public Task<int> CompleteAsync();
    }
}
