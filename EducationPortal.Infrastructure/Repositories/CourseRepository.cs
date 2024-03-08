namespace EducationPortal.Infrastructure.Repositories
{
    public class CourseRepository : Repository<CourseModel>, ICourseRepository
    {
        public EducationPortalContext EPContext
        {
            get { return Context as EducationPortalContext; }
        }
        public CourseRepository(EducationPortalContext context) : base(context) { }
        public async Task<CourseModel> GetWithFullInfoAsync(int id)
        {
            return await EPContext.Courses
                .Where(c => c.IsDeleted == false)
                .Where(c => c.Id == id)
                .Include(c => c.CourseSkills)
                .ThenInclude(cs => cs.Skill)
                .Include(c => c.CourseMaterials)
                .ThenInclude(cm => cm.Material)
                .FirstOrDefaultAsync();
        }
        public async Task<ICollection<CourseModel>> GetAllWithFullInfoAsync()
        {
            return await EPContext.Courses
                .Where(c => c.IsDeleted == false)
                .Include(c => c.CourseSkills)
                .ThenInclude(cs => cs.Skill)
                .Include(c => c.CourseMaterials)
                .ThenInclude(cm => cm.Material)
                .ToListAsync();
        }
        public async Task<CourseModel> FindWithFullInfoAsync(Expression<Func<CourseModel, bool>> predicate)
        {
            return await EPContext.Courses
                .Where(predicate)
                .Where(c => c.IsDeleted == false)
                .Include(c => c.CourseSkills)
                .ThenInclude(cs => cs.Skill)
                .Include(c => c.CourseMaterials)
                .ThenInclude(cm => cm.Material)
                .FirstOrDefaultAsync();
        }
        public async Task<ICollection<CourseModel>> FindManyWithFullInfoAsync(Expression<Func<CourseModel, bool>> predicate)
        {
            return await EPContext.Courses
                .Where(predicate)
                .Where(c => c.IsDeleted == false)
                .Include(c => c.CourseSkills)
                .ThenInclude(cs => cs.Skill)
                .Include(c => c.CourseMaterials)
                .ThenInclude(cm => cm.Material)
                .ToListAsync();
        }
        public async Task<bool> IsNameUniqueAsync(string name)
        {
            return !await EPContext.Courses.AnyAsync(c => c.Name == name);
        }
    }
}
