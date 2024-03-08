namespace EducationPortal.Domain.Entities
{
    public class Skill
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Level { get; set; } = 0;
        public Skill() { }
        public Skill (string name)
        {
            Name = name;
        }
    }
}
