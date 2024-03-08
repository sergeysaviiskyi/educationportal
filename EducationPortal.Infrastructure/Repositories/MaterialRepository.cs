namespace EducationPortal.Infrastructure.Repositories
{
    public class MaterialRepository : Repository<MaterialModel>, IMaterialRepository
    {
        public EducationPortalContext EPContext
        {
            get { return Context as EducationPortalContext; }
        }
        public MaterialRepository(EducationPortalContext context) : base(context) { }

        public async Task<bool> IsNameUniqueAsync(string name)
        {
            return !await EPContext.Materials.AnyAsync(c => c.Name == name);
        }
    }
}
