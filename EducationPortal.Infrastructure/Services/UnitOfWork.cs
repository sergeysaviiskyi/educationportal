namespace EducationPortal.Infrastructure.Services
{
    public class UnitOfWork : IUnitOfWork
    {
        public IUserRepository Users { get; private set; }
        public ICourseRepository Courses { get; private set; }
        public IMaterialRepository Materials { get; private set; }
        public ISkillRepository Skills { get; private set; }
        public IUserCourseRepository UserCourses { get; private set; }
        public IUserSkillRepository UsersSkills { get; private set; }
        public ICourseMaterialRepository CourseMaterials { get; private set; }
        public ICourseSkillRepository CourseSkills { get; private set; }
        public IVerificationCodeRepository VerificationCode { get; private set; }

        private readonly EducationPortalContext _context;
        public UnitOfWork(EducationPortalContext context)
        {
            _context = context;
            Users = new UserRepository(_context);
            Courses = new CourseRepository(_context);
            Materials = new MaterialRepository(_context);
            Skills = new SkillRepository(_context);
            UserCourses = new UserCourseRepository(_context);
            UsersSkills = new UserSkillRepository(_context);
            CourseMaterials = new CourseMaterialRepository(_context);
            CourseSkills = new CourseSkillRepository(_context);
            VerificationCode = new VerificatonCodeRepository(_context);
        }
        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
