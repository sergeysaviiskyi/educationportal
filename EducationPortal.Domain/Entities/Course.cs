namespace EducationPortal.Domain.Entities
{
    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Length { get; set; } = 0;
        public int LearningProgress { get; set; } = 0;
        public int Status { get; set; }
        public List<Material> CourseMaterials { get; set; } = new List<Material>();
        public List<Skill> CourseSkills { get; set; } = new List<Skill>();
        public Course() { }
        public Course(string name, string description)
        {
            Name = name;
            Description = description;
        }
    }
}