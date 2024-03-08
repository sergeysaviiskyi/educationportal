namespace EducationPortal.Domain.Models
{
    public class UserModel : DataBaseEntity
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public byte[] Salt { get; set; }
        public byte[] Password { get; set; }
        public int RefreshTokenId { get; set; }
        public int? VerificationCodeId { get; set; }
        public RefreshTokenModel RefreshToken { get; set; }
        public VerificationCodeModel VerificationCode { get; set; }
        public ICollection<UserCourseModel> Courses { get; set; }
        public ICollection<UserSkillModel> LearnedSkills { get; set; }
    }
}
