namespace EducationPortal.Infrastructure.Repositories
{
    public class CourseSkillRepository : Repository<CourseSkillModel>, ICourseSkillRepository
    {
        public EducationPortalContext EPContext
        {
            get { return Context as EducationPortalContext; }
        }
        public CourseSkillRepository(EducationPortalContext context) : base(context) { }
    }
}
