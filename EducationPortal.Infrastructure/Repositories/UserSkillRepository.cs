namespace EducationPortal.Infrastructure.Repositories
{
    public class UserSkillRepository : Repository<UserSkillModel>, IUserSkillRepository
    {
        public EducationPortalContext EPContextЫ
        {
            get { return Context as EducationPortalContext; }
        }
        public UserSkillRepository(EducationPortalContext context) : base(context) { }
    }
}
