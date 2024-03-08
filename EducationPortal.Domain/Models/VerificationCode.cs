namespace EducationPortal.Domain.Models
{
    public class VerificationCodeModel : DataBaseEntity
    {
        public byte[] Salt { get; set; }
        public byte[] Code { get; set; }
        public DateTime ExpiryDate { get; set; }
        public UserModel User { get; set; }
    }
}
