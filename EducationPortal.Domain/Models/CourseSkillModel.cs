namespace EducationPortal.Domain.Models
{
    public class CourseSkillModel : DataBaseEntity
    {
        public int CourseId { get; set; }
        public int SkillId { get; set; }
        public CourseModel Course { get; set; }
        public SkillModel Skill { get; set; }
    }
}
