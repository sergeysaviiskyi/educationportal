namespace EducationPortal.Application.Interfaces.Services
{
    public interface ISkillService
    {
        public Task<Result<Skill>> GetAsync(int id);
        public Task<Result<IEnumerable<Skill>>> GetAllAsync();
        public Task<Result<Skill>> CreateAsync(string name);
        public Task<Result> RemoveAsync(int id);
    }
}