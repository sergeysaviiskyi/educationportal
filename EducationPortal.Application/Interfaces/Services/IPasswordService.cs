namespace EducationPortal.Application.Interfaces.Services
{
    public interface IPasswordService
    {
        public byte[] HashPassword(byte[] password, byte[] salt);
        public bool VerifyHashedPassword(byte[] password, byte[] salt, byte[] exitingCode);
        public byte[] GenerateSalt();
    }
}
