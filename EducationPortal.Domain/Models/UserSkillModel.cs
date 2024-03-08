namespace EducationPortal.Domain.Models
{
    public class UserSkillModel : DataBaseEntity
    {
        public int UserId { get; set; }
        public int SkillId { get; set; }
        public UserModel User { get; set; }
        public SkillModel Skill { get; set; }
        public int Level { get; set; } = 1;
    }
}
