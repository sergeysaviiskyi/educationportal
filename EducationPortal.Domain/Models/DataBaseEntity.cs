namespace EducationPortal.Domain.Models
{
    public abstract class DataBaseEntity
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
