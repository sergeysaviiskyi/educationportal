namespace EducationPortal.Infrastructure.Repositories
{
    public class UserRepository : Repository<UserModel>, IUserRepository
    {
        public EducationPortalContext EPContext
        {
            get { return Context as EducationPortalContext; }
        }
        public UserRepository(EducationPortalContext context) : base(context) { }
        public async Task<UserModel> GetWithFullInfoAsync(int id)
        {
            return await EPContext.Users
                .Where(u => u.IsDeleted == false)
                .Include(u => u.LearnedSkills)
                .ThenInclude(us => us.Skill)
                .Include(u => u.Courses)
                .ThenInclude(uc => uc.Course)
                .ThenInclude(c => c.CourseMaterials)
                .ThenInclude(m => m.Material)
                .Include(u => u.Courses)
                .ThenInclude(uc => uc.Course)
                .ThenInclude(c => c.CourseSkills)
                .ThenInclude(m => m.Skill)
                .Include(u => u.RefreshToken)
                .Include(u => u.VerificationCode)
                .FirstOrDefaultAsync(u => u.Id == id);
        }
        public async Task<ICollection<UserModel>> GetAllWithFullInfoAsync()
        {
            return await EPContext.Users
                .Where(u => u.IsDeleted == false)
                .Include(u => u.LearnedSkills)
                .ThenInclude(us => us.Skill)
                .Include(u => u.Courses)
                .ThenInclude(uc => uc.Course)
                .ThenInclude(c => c.CourseMaterials)
                .ThenInclude(m => m.Material)
                .Include(u => u.Courses)
                .ThenInclude(uc => uc.Course)
                .ThenInclude(c => c.CourseSkills)
                .ThenInclude(m => m.Skill)
                .Include(u => u.RefreshToken)
                .Include(u => u.VerificationCode)
                .ToListAsync();
        }
        public async Task<UserModel> FindWithFullinfoAsync(Expression<Func<UserModel, bool>> predicate)
        {
            return await EPContext.Users
                .Where(predicate)
                .Where(u => u.IsDeleted == false)
                .Include(u => u.LearnedSkills)
                .ThenInclude(us => us.Skill)
                .Include(u => u.Courses)
                .ThenInclude(uc => uc.Course)
                .ThenInclude(c => c.CourseMaterials)
                .ThenInclude(m => m.Material)
                .Include(u => u.Courses)
                .ThenInclude(uc => uc.Course)
                .ThenInclude(c => c.CourseSkills)
                .ThenInclude(m => m.Skill)
                .Include(u => u.RefreshToken)
                .Include(u => u.VerificationCode)
                .FirstOrDefaultAsync();
        }
        public async Task<ICollection<UserModel>> FindManyWithFullinfoAsync(Expression<Func<UserModel, bool>> predicate)
        {
            return await EPContext.Users
                .Where(predicate)
                .Where(u => u.IsDeleted == false)
                .Include(u => u.LearnedSkills)
                .ThenInclude(us => us.Skill)
                .Include(u => u.Courses)
                .ThenInclude(uc => uc.Course)
                .ThenInclude(c => c.CourseMaterials)
                .ThenInclude(m => m.Material)
                .Include(u => u.Courses)
                .ThenInclude(uc => uc.Course)
                .ThenInclude(c => c.CourseSkills)
                .ThenInclude(m => m.Skill)
                .Include(u => u.RefreshToken)
                .Include(u => u.VerificationCode)
                .ToListAsync();
        }
        public async Task<bool> IsExistingEmail(string email)
        {
            return await EPContext.Users.AnyAsync(u => u.Email == email);
        }
        public async Task<bool> CanLearnCourse(int userId, int corseId)
        {
            var user = await GetWithFullInfoAsync(userId);
            if (user == null) return false;
            return user.Courses.Any(uc => uc.CourseId == corseId) ? false : true;
        }
    }
}