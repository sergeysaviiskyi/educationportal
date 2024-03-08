namespace EducationPortal.Infrastructure.Repositories
{
    internal class UserCourseRepository : Repository<UserCourseModel>, IUserCourseRepository
    {
        public EducationPortalContext EPContext
        {
            get { return Context as EducationPortalContext; }
        }
        public UserCourseRepository(EducationPortalContext context) : base(context) { }
    }
}
