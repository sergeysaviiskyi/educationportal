namespace EducationPortal.Infrastructure.Repositories
{
    public class SkillRepository : Repository<SkillModel>, ISkillRepository
    {
        public EducationPortalContext EPContext
        {
            get { return Context as EducationPortalContext; }
        }
        public SkillRepository(EducationPortalContext context) : base(context) { }

        public async Task<bool> IsNameUniqueAsync(string name)
        {
            return !await EPContext.Skills.AnyAsync(c => c.Name == name);
        }
    }
}
