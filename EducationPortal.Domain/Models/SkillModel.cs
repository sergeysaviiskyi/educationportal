namespace EducationPortal.Domain.Models
{
    public class SkillModel : DataBaseEntity
    {
        public string Name { get; set; }
        public ICollection<UserSkillModel> LearnedByUsers { get; set; }
        public ICollection<CourseSkillModel> Courses { get; set; }
    }
}
