namespace EducationPortal.Domain.Models
{
    public class UserCourseModel : DataBaseEntity
    {
        public int UserId { get; set; }
        public int CourseId { get; set; }
        public UserModel User { get; set; }
        public CourseModel Course { get; set; }
        public int Status { get; set; } = 0;
        public int LearningProgress { get; set; } = 0;
    }
}
