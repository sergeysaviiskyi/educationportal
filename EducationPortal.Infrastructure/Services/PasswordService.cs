namespace EducationPortal.Infrastructure.Services
{
    public class PasswordService : IPasswordService
    {
        public byte[] HashPassword(byte[] password, byte[] salt)
        {
            using var argon2 = new Argon2id(password);
            argon2.Salt = salt;
            argon2.DegreeOfParallelism = 8;
            argon2.Iterations = 4;
            argon2.MemorySize = 1024 * 128;
            return argon2.GetBytes(32);
        }
        public bool VerifyHashedPassword(byte[] password, byte[] salt, byte[] exitingCode)
        {
            return HashPassword(password, salt).SequenceEqual(exitingCode);
        }
        public byte[] GenerateSalt()
        {
            var salt = new byte[32];
            using var generator = RandomNumberGenerator.Create();
            generator.GetBytes(salt);
            return salt;
        }
    }
}