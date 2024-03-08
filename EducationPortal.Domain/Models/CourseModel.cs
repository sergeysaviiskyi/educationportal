namespace EducationPortal.Domain.Models
{
    public class CourseModel : DataBaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Length { get; set; } = 0;
        public ICollection<UserCourseModel> Users { get; set; }
        public ICollection<CourseMaterialModel> CourseMaterials { get; set; }
        public ICollection<CourseSkillModel> CourseSkills { get; set; }
    }
}
