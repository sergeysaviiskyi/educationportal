namespace EducationPortal.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public List<Course> CoursesInProgress { get; set; } = new List<Course>();
        public List<Course> FinishedCourses { get; set; } = new List<Course>();
        public List<Skill> LearnedSkills { get; set; } = new List<Skill>();
        public User(string name, string email)
        {
            Name = name;
            Email = email;
        }
    }
}
