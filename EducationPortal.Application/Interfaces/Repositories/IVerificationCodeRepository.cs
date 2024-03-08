namespace EducationPortal.Application.Interfaces.Repositories
{
    public interface IVerificationCodeRepository : IGenericRepository<VerificationCodeModel>
    {
        public Task RemoveNotValid();
    }
}
