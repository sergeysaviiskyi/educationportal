namespace EducationPortal.Domain.Entities.Materials
{
    public class Material
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public Material() { }
        public Material(string name)
        {
            Name = name;
        }
    }
}
