namespace EducationPortal.Infrastructure.Repositories
{
    public class VerificatonCodeRepository : Repository<VerificationCodeModel>, IVerificationCodeRepository
    {
        public EducationPortalContext EPContext
        {
            get { return Context as EducationPortalContext; }
        }
        public VerificatonCodeRepository(EducationPortalContext context) : base(context) { }

        public async Task RemoveNotValid()
        {
            var codes = await EPContext.VerificationCodes.Where(c => c.ExpiryDate <= DateTime.Now).ToListAsync();
            EPContext.VerificationCodes.RemoveRange(codes);
        }
    }
}
