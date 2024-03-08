namespace EducationPortal.Domain.Models
{
    public class CourseMaterialModel : DataBaseEntity
    {
        public int CourseId { get; set; }
        public int MaterialId { get; set; }
        public CourseModel Course { get; set; }
        public MaterialModel Material { get; set; }
    }
}
