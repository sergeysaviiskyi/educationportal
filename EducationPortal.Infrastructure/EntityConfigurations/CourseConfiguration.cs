namespace EducationPortal.Infrastructure.EntityConfigurations
{
    public class CourseConfiguration : IEntityTypeConfiguration<CourseModel>
    {
        public void Configure(EntityTypeBuilder<CourseModel> builder)
        {
            builder.HasMany(c => c.Users)
                .WithOne(uc => uc.Course)
                .HasForeignKey(uc => uc.CourseId);

            builder.HasMany(c => c.CourseMaterials)
                .WithOne(cm => cm.Course)
                .HasForeignKey(cm => cm.CourseId);

            builder.HasMany(c => c.CourseSkills)
                .WithOne(cm => cm.Course)
                .HasForeignKey(cm => cm.CourseId);
        }
    }
}
