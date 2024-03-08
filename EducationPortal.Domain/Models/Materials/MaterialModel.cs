namespace EducationPortal.Domain.Models.Materials
{
    public class MaterialModel : DataBaseEntity
    {
        public string Name { get; set; }
        public ICollection<CourseMaterialModel> Courses { get; set; }
    }
}