namespace EducationPortal.Infrastructure.Repositories
{
    public class CourseMaterialRepository : Repository<CourseMaterialModel>, ICourseMaterialRepository
    {
        public EducationPortalContext EPContext
        {
            get { return Context as EducationPortalContext; }
        }
        public CourseMaterialRepository(EducationPortalContext context) : base(context) { }
    }
}
