namespace EducationPortal.Infrastructure.EntityConfigurations
{
    public class CourseMaterialConfiguration : IEntityTypeConfiguration<CourseMaterialModel>
    {
        public void Configure(EntityTypeBuilder<CourseMaterialModel> builder)
        {
            builder.HasIndex(cm => new { cm.CourseId, cm.MaterialId })
                .IsUnique();
        }
    }
}
