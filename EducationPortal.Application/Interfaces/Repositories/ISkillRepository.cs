using System;

namespace EducationPortal.Application.Interfaces.Repositories
{
    public interface ISkillRepository : IGenericRepository<SkillModel>
    {
        public Task<bool> IsNameUniqueAsync(string name);
    }
}
