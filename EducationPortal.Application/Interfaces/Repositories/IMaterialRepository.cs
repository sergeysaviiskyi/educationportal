namespace EducationPortal.Application.Interfaces.Repositories
{
    public interface IMaterialRepository : IGenericRepository<MaterialModel>
    {
        public Task<bool> IsNameUniqueAsync(string name);
    }
}
